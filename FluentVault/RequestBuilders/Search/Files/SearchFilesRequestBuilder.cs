using FluentVault.Domain.Search;
using FluentVault.Domain.Search.Files;
using FluentVault.Features;
using FluentVault.RequestBuilders;

using MediatR;

namespace FluentVault.Requests.Search.Files;

internal class SearchFilesRequestBuilder : 
    IRequestBuilder,
    ISearchFilesRequestBuilder,
    ISearchFilesOperatorSelector,
    ISearchFilesEndpoint
{
    private readonly IMediator _mediator;

    private object? _value = new();
    private VaultPropertyDefinitionId? _propertyId;
    private string _propertyName = string.Empty;
    private SearchPropertyType _propertyType = SearchPropertyType.SingleProperty;
    private SearchOperator _operator = SearchOperator.Contains;
    private IEnumerable<VaultProperty> _allProperties = new List<VaultProperty>();
    private bool _recurseFolders = true;
    private bool _latestOnly = true;
    private readonly List<VaultFolderId> _folderIds = new();
    private readonly List<SearchCondition> _searchConditions = new();
    private readonly List<SortCondition> _sortConditions = new();

    public SearchFilesRequestBuilder(IMediator mediator)
    {
        _mediator = mediator;
    }

    public ISearchFilesEndpoint EqualTo(object? value) => SetValueAndOperator(value, SearchOperator.IsEqualTo);
    public ISearchFilesEndpoint Containing(object? value) => SetValueAndOperator(value, SearchOperator.Contains);
    public ISearchFilesEndpoint NotEqualTo(object? value) => SetValueAndOperator(value, SearchOperator.IsNotEqualTo);
    public ISearchFilesEndpoint LessThan(object? value) => SetValueAndOperator(value, SearchOperator.IsLessThan);
    public ISearchFilesEndpoint LessThanOrEqualTo(object? value) => SetValueAndOperator(value, SearchOperator.IsLessThanOrEqualTo);
    public ISearchFilesEndpoint GreaterThan(object? value) => SetValueAndOperator(value, SearchOperator.IsGreaterThan);
    public ISearchFilesEndpoint GreaterThanOrEqualTo(object? value) => SetValueAndOperator(value, SearchOperator.IsGreaterThanOrEqualTo);
    
    public ISearchFilesOperatorSelector BySystemProperty(VaultSearchProperty property)
    {
        _propertyId = property;
        _propertyType = SearchPropertyType.SingleProperty;

        return this;
    }

    public ISearchFilesOperatorSelector ByUserProperty(string name)
    {
        _propertyName = name;

        return this;
    }

    public ISearchFilesOperatorSelector ByAllProperties
    {
        get
        {
            _propertyType = SearchPropertyType.AllProperties;

            return this;
        }
    }

    public ISearchFilesOperatorSelector ByAllPropertiesAndContent
    {
        get
        {
            _propertyType = SearchPropertyType.AllPropertiesAndContent;

            return this;
        }
    }

    public ISearchFilesEndpoint AllVersions
    {
        get
        {
            _latestOnly = false;

            return this;
        }
    }

    public ISearchFilesRequestBuilder And
    {
        get
        {
            AddSearchCondition();

            return this;
        }
    }

    public async Task<IEnumerable<VaultFile>> GetAllResultsAsync()
    {
        IEnumerable<VaultFile> files = await SearchAsync(int.MaxValue);

        return files;
    }

    public async Task<IEnumerable<VaultFile>> GetPagedResultAsync(int maxResultCount = 200)
    {
        IEnumerable<VaultFile> files = await SearchAsync(maxResultCount);

        return files;
    }

    public async Task<VaultFile?> GetFirstResultAsync()
    {
        IEnumerable<VaultFile> files = await GetPagedResultAsync();

        return files.FirstOrDefault();
    }

    private async Task SetPropertyValue(string property)
    {
        if (_allProperties.Any() is false)
            _allProperties = await _mediator.Send(new GetAllPropertyDefinitionInfosQuery());

        VaultProperty selectedProperty = _allProperties.FirstOrDefault(x => x.Definition.DisplayName.Equals(property))
            ?? throw new KeyNotFoundException($@"Property ""{property}"" was not found");

        _propertyId = selectedProperty.Definition.Id;
    }

    private async Task<IEnumerable<VaultFile>> SearchAsync(int maxResultCount)
    {
        if (string.IsNullOrEmpty(_propertyName) is false)
            await SetPropertyValue(_propertyName);

        AddSearchCondition();

        List<VaultFile> files = new();
        string bookmark = string.Empty;
        IEnumerable<IDictionary<string, object>> searchConditionAttributes = _searchConditions.Select(x => x.Attributes);
        IEnumerable<IDictionary<string, object>> sortConditionAttributes = _sortConditions.Select(x => x.Attributes);

        do
        {
            FindFilesBySearchConditionsQuery command = new(searchConditionAttributes, sortConditionAttributes, _folderIds, _recurseFolders, _latestOnly, bookmark);
            VaultSearchFilesResponse response = await _mediator.Send(command);
            files.AddRange(response.Result.Files);
            bookmark = response.Bookmark;
        } while (files.Count <= maxResultCount && string.IsNullOrEmpty(bookmark) is false);

        return files;
    }

    private void AddSearchCondition()
    {
        if (_propertyId is not null)
        {
            SearchCondition searchCondition = new(_propertyId, _operator, _value, _propertyType, SearchRule.Must);
            _searchConditions.Add(searchCondition);
        }
        _propertyType = SearchPropertyType.SingleProperty;
    }

    private ISearchFilesEndpoint SetValueAndOperator(object? value, SearchOperator @operator)
    {
        _value = value;
        _operator = @operator;

        return this;
    }
}
