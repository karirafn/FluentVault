using FluentVault.Domain.Search;
using FluentVault.Domain.Search.Files;
using FluentVault.Features;
using FluentVault.RequestBuilders;
using FluentVault.RequestBuilders.Search;

using MediatR;

namespace FluentVault.Requests.Search.Files;

internal class SearchFilesRequestBuilder : 
    IRequestBuilder,
    ISearchFilesRequestBuilder,
    ISearchFilesOperatorSelector,
    ISearchFilesEndpoint
{
    private readonly IMediator _mediator;
    private readonly ISearchManager _searchManager;

    public SearchFilesRequestBuilder(IMediator mediator, ISearchManager searchManager)
    {
        _mediator = mediator;
        _searchManager = searchManager;
    }

    public ISearchFilesEndpoint EqualTo(object? value) => SetValueAndOperator(value, SearchOperator.IsEqualTo);
    public ISearchFilesEndpoint Containing(object? value) => SetValueAndOperator(value, SearchOperator.Contains);
    public ISearchFilesEndpoint NotEqualTo(object? value) => SetValueAndOperator(value, SearchOperator.IsNotEqualTo);
    public ISearchFilesEndpoint LessThan(object? value) => SetValueAndOperator(value, SearchOperator.IsLessThan);
    public ISearchFilesEndpoint LessThanOrEqualTo(object? value) => SetValueAndOperator(value, SearchOperator.IsLessThanOrEqualTo);
    public ISearchFilesEndpoint GreaterThan(object? value) => SetValueAndOperator(value, SearchOperator.IsGreaterThan);
    public ISearchFilesEndpoint GreaterThanOrEqualTo(object? value) => SetValueAndOperator(value, SearchOperator.IsGreaterThanOrEqualTo);
    
    public ISearchFilesOperatorSelector ByPropertyId(VaultPropertyDefinitionId id)
    {
        _searchManager.SearchConditionPropertyId = id;
        _searchManager.PropertyType = SearchPropertyType.SingleProperty;
        return this;
    }

    public ISearchFilesOperatorSelector BySystemProperty(VaultSearchProperty property)
        => ByPropertyId(property.Value);

    public ISearchFilesOperatorSelector ByAllProperties
    {
        get
        {
            _searchManager.PropertyType = SearchPropertyType.AllProperties;
            return this;
        }
    }

    public ISearchFilesOperatorSelector ByAllPropertiesAndContent
    {
        get
        {
            _searchManager.PropertyType = SearchPropertyType.AllPropertiesAndContent;
            return this;
        }
    }

    public ISearchFilesEndpoint AllVersions
    {
        get
        {
            _searchManager.LatestOnly = false;
            return this;
        }
    }

    public ISearchFilesRequestBuilder And
    {
        get
        {
            _searchManager.AddSearchCondition();
            return this;
        }
    }

    public async Task<IEnumerable<VaultFile>> GetAllResultsAsync()
    {
        IEnumerable<VaultFile> files = await GetPagedResultAsync(int.MaxValue);
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

    private async Task<IEnumerable<VaultFile>> SearchAsync(int maxResultCount)
    {
        _searchManager.AddSearchCondition();

        List<VaultFile> files = new();
        string bookmark = string.Empty;
        do
        {
            FindFilesBySearchConditionsQuery query = new(
                _searchManager.SearchConditions,
                _searchManager.SortConditions,
                _searchManager.FolderIds,
                _searchManager.RecurseFolders,
                _searchManager.LatestOnly,
                bookmark);
            VaultSearchFilesResponse response = await _mediator.Send(query);
            files.AddRange(response.Result.Files);
            bookmark = response.Bookmark;
        } while (files.Count <= maxResultCount && string.IsNullOrEmpty(bookmark) is false);
        return files;
    }

    private ISearchFilesEndpoint SetValueAndOperator(object? value, SearchOperator @operator)
    {
        _searchManager.SearchValue = value;
        _searchManager.SearchOperator = @operator;
        return this;
    }
}
