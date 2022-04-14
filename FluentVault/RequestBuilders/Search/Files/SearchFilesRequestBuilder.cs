using FluentVault.Domain.Search;
using FluentVault.Features;

using MediatR;

namespace FluentVault.Requests.Search.Files;

internal class SearchFilesRequestBuilder :
    ISearchFilesRequestBuilder,
    ISearchFilesBooleanProperty,
    ISearchFilesDateTimeProperty,
    ISearchFilesNumericProperty,
    ISearchFilesStringProperty,
    ISearchFilesAddSearchCondition
{
    private readonly IMediator _mediator;

    private object _value = new();
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
        => _mediator = mediator;

    public async Task<IEnumerable<VaultFile>> WithoutPaging()
    {
        IEnumerable<VaultFile> files = await SearchAsync(int.MaxValue);
        return files;
    }

    public async Task<IEnumerable<VaultFile>> WithPaging(int maxResultCount = 200)
    {
        IEnumerable<VaultFile> files = await SearchAsync(maxResultCount);
        return files;
    }

    public async Task<VaultFile?> SearchSingleAsync()
    {
        IEnumerable<VaultFile> files = await WithPaging();
        return files.FirstOrDefault();
    }

    public ISearchFilesBooleanProperty ForValueEqualTo(bool value) => SetBooleanValue(value, SearchOperator.IsEqualTo);
    public ISearchFilesBooleanProperty ForValueNotEqualTo(bool value) => SetBooleanValue(value, SearchOperator.IsNotEqualTo);

    public ISearchFilesDateTimeProperty ForValueEqualTo(DateTime value) => SetDateTimeValue(value, SearchOperator.IsEqualTo);
    public ISearchFilesDateTimeProperty ForValueNotEqualTo(DateTime value) => SetDateTimeValue(value, SearchOperator.IsNotEqualTo);
    public ISearchFilesDateTimeProperty ForValueLessThan(DateTime value) => SetDateTimeValue(value, SearchOperator.IsLessThan);
    public ISearchFilesDateTimeProperty ForValueLessThanOrEqualTo(DateTime value) => SetDateTimeValue(value, SearchOperator.IsLessThanOrEqualTo);
    public ISearchFilesDateTimeProperty ForValueGreaterThan(DateTime value) => SetDateTimeValue(value, SearchOperator.IsGreaterThan);
    public ISearchFilesDateTimeProperty ForValueGreaterThanOrEqualTo(DateTime value) => SetDateTimeValue(value, SearchOperator.IsGreaterThanOrEqualTo);

    public ISearchFilesNumericProperty ForValueEqualTo(int value) => SetNumericValue(value, SearchOperator.IsEqualTo);
    public ISearchFilesNumericProperty ForValueNotEqualTo(int value) => SetNumericValue(value, SearchOperator.IsNotEqualTo);
    public ISearchFilesNumericProperty ForValueLessThan(int value) => SetNumericValue(value, SearchOperator.IsLessThan);
    public ISearchFilesNumericProperty ForValueLessThanOrEqualTo(int value) => SetNumericValue(value, SearchOperator.IsLessThanOrEqualTo);
    public ISearchFilesNumericProperty ForValueGreaterThan(int value) => SetNumericValue(value, SearchOperator.IsGreaterThan);
    public ISearchFilesNumericProperty ForValueGreaterThanOrEqualTo(int value) => SetNumericValue(value, SearchOperator.IsGreaterThanOrEqualTo);

    public ISearchFilesNumericProperty ForValueEqualTo(double value) => SetNumericValue(value, SearchOperator.IsEqualTo);
    public ISearchFilesNumericProperty ForValueNotEqualTo(double value) => SetNumericValue(value, SearchOperator.IsNotEqualTo);
    public ISearchFilesNumericProperty ForValueLessThan(double value) => SetNumericValue(value, SearchOperator.IsLessThan);
    public ISearchFilesNumericProperty ForValueLessThanOrEqualTo(double value) => SetNumericValue(value, SearchOperator.IsLessThanOrEqualTo);
    public ISearchFilesNumericProperty ForValueGreaterThan(double value) => SetNumericValue(value, SearchOperator.IsGreaterThan);
    public ISearchFilesNumericProperty ForValueGreaterThanOrEqualTo(double value) => SetNumericValue(value, SearchOperator.IsGreaterThanOrEqualTo);

    public ISearchFilesStringProperty ForValueEqualTo(string value) => SetStringValue(value, SearchOperator.IsEqualTo);
    public ISearchFilesStringProperty ForValueContaining(string value) => SetStringValue(value, SearchOperator.Contains);
    public ISearchFilesStringProperty ForValueNotContaining(string value) => SetStringValue(value, SearchOperator.DoesNotContain);

    public ISearchFilesAddSearchCondition InSystemProperty(BooleanSearchProperty property) => SetProperty(property);
    public ISearchFilesAddSearchCondition InSystemProperty(DateTimeSearchProperty property) => SetProperty(property);
    public ISearchFilesAddSearchCondition InSystemProperty(NumericSearchProperty property) => SetProperty(property);
    public ISearchFilesAddSearchCondition InSystemProperty(StringSearchProperty property) => SetProperty(property);

    public ISearchFilesAddSearchCondition InUserProperty(string name)
    {
        _propertyName = name;
        return this;
    }

    public ISearchFilesAddSearchCondition InAllProperties
    {
        get
        {
            _propertyType = SearchPropertyType.AllProperties;
            return this;
        }
    }

    public ISearchFilesAddSearchCondition InAllPropertiesAndContent
    {
        get
        {
            _propertyType = SearchPropertyType.AllPropertiesAndContent;
            return this;
        }
    }

    public ISearchFilesAddSearchCondition GetAllVersions()
    {
        _latestOnly = false;
        return this;
    }

    public ISearchFilesRequestBuilder And
    {
        get
        {
            AddSearchCondition();
            return this;
        }
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

    private ISearchFilesBooleanProperty SetBooleanValue(bool value, SearchOperator @operator)
    {
        SetValue(value, @operator);
        return this;
    }

    private ISearchFilesDateTimeProperty SetDateTimeValue(DateTime value, SearchOperator @operator)
    {
        SetValue(value, @operator);
        return this;
    }

    private ISearchFilesNumericProperty SetNumericValue(int value, SearchOperator @operator)
    {
        SetValue(value, @operator);
        return this;
    }

    private ISearchFilesNumericProperty SetNumericValue(double value, SearchOperator @operator)
    {
        SetValue(value, @operator);
        return this;
    }

    private ISearchFilesStringProperty SetStringValue(string value, SearchOperator @operator)
    {
        SetValue(value, @operator);
        return this;
    }

    private void SetValue(object value, SearchOperator @operator)
        => (_value, _operator) = (value, @operator);

    private ISearchFilesAddSearchCondition SetProperty(VaultSearchProperty property)
    {
        (_propertyId, _propertyType) = (property, SearchPropertyType.SingleProperty);
        return this;
    }
}
