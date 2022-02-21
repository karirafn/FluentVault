using System.Globalization;
using System.Text;
using System.Xml.Linq;

using FluentVault.Common.Extensions;
using FluentVault.Common.Helpers;
using FluentVault.Domain.Common;
using FluentVault.Domain.Search;
using FluentVault.Requests.Get.Properties;

namespace FluentVault.Requests.Search.Files;

internal class SearchFilesRequest : SessionRequest,
    ISearchFilesRequest,
    ISearchFilesBooleanProperty,
    ISearchFilesDateTimeProperty,
    ISearchFilesNumericProperty,
    ISearchFilesStringProperty,
    ISearchFilesAddSearchCondition
{
    private object _searchValue = new();
    private long _property;
    private long _operator;
    private string _propertyName = string.Empty;
    private SearchPropertyType _propertyType = SearchPropertyType.SingleProperty;
    private IEnumerable<VaultPropertyDefinition> _allProperties = new List<VaultPropertyDefinition>();
    private bool _recurseFolders = true;
    private bool _latestOnly = true;
    private readonly List<long> _folderIds = new();
    private readonly List<IDictionary<string, string>> _searchConditions = new();
    private readonly List<IDictionary<string, string>> _sortConditions = new();

    public SearchFilesRequest(VaultSession session)
        : base(session, RequestData.FindFilesBySearchConditions) { }

    public async Task<IEnumerable<VaultFile>> SearchWithoutPaging()
    {
        IEnumerable<VaultFile> files = await SearchAsync(int.MaxValue);
        return files;
    }

    public async Task<IEnumerable<VaultFile>> SearchWithPaging(int maxResultCount = 200)
    {
        IEnumerable<VaultFile> files = await SearchAsync(maxResultCount);
        return files;
    }

    public async Task<VaultFile?> SearchSingleAsync()
    {
        IEnumerable<VaultFile> files = await SearchWithPaging();
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

    public ISearchFilesAddSearchCondition InUserProperty(string name)
    {
        _propertyName = name;
        return this;
    }

    public ISearchFilesAddSearchCondition InSystemProperty(SearchBooleanProperties property) => SetProperty((long)property);
    public ISearchFilesAddSearchCondition InSystemProperty(SearchDateTimeProperty property) => SetProperty((long)property);
    public ISearchFilesAddSearchCondition InSystemProperty(SearchNumericProperty property) => SetProperty((long)property);
    public ISearchFilesAddSearchCondition InSystemProperty(SearchStringProperty property) => SetProperty((long)property);

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

    public ISearchFilesRequest And
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
            _allProperties = await new GetPropertiesRequest(Session).SendAsync();

        var selectedProperty = _allProperties.FirstOrDefault(x => x.Definition.DisplayName.Equals(property))
            ?? throw new KeyNotFoundException($@"Property ""{property}"" was not found");
        _property = selectedProperty.Definition.Id;
    }

    private async Task<IEnumerable<VaultFile>> SearchAsync(int maxResultCount)
    {
        if (string.IsNullOrEmpty(_propertyName) is false)
            await SetPropertyValue(_propertyName);

        AddSearchCondition();

        List<VaultFile> files = new();
        string bookmark = string.Empty;
        do
        {
            StringBuilder innerBody = GetInnerBody(bookmark);
            XDocument document = await SendRequestAsync(innerBody);
            VaultFileSearchResult result = document.ParseFileSearchResult();
            files.AddRange(result.Files);
            bookmark = result.Bookmark;
        } while (files.Count <= maxResultCount && string.IsNullOrEmpty(bookmark) is false);

        return files;
    }

    private StringBuilder GetInnerBody(string bookmark)
        => new StringBuilder()
            .AppendElementWithAttribute(RequestData.Name, "xmlns", RequestData.Namespace)
            .AppendNestedElementsWithAttributes("conditions", "SrchCond", _searchConditions)
            .AppendNestedElementsWithAttributes("sortConditions", "SrchSort", _searchConditions)
            .AppendElements("folderIds", _folderIds)
            .AppendElement("recurseFolders", _recurseFolders)
            .AppendElement("latestOnly", _latestOnly)
            .AppendElement("bookmark", bookmark)
            .AppendElementClosing(RequestData.Name);

    private void AddSearchCondition()
    {
        string value = _searchValue switch
        {
            string s => s,
            DateTime d => d.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
            not null => _searchValue.ToString() ?? throw new Exception("Unable to convert search value to string"),
            _ => string.Empty
        };

        var searchCondition = GetSearchCondition(_property, _operator, value, _propertyType, SearchRule.Must);
        _searchConditions.Add(searchCondition);
        _propertyType = SearchPropertyType.SingleProperty;
    }

    private static IDictionary<string, string> GetSearchCondition(long propertyId, long searchOperator, string searchText, SearchPropertyType type, SearchRule rule)
        => new Dictionary<string, string>
        {
            { "PropDefId", propertyId.ToString() },
            { "SrchOper", searchOperator.ToString() },
            { "SrchTxt", searchText },
            { "PropTyp", type.ToString() },
            { "SrchRule", rule.ToString() },
        };

    private static IDictionary<string, string> GetSortCondition(long propertyId, bool sortAscending)
        => new Dictionary<string, string>
        {
            { "PropDefId", propertyId.ToString() },
            { "SortAsc", sortAscending.ToString().ToLower() },
        };

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
    {
        _searchValue = value;
        _operator = (long)@operator;
    }

    private ISearchFilesAddSearchCondition SetProperty(long value)
    {
        _property = value;
        _propertyType = SearchPropertyType.SingleProperty;
        return this;
    }
}
