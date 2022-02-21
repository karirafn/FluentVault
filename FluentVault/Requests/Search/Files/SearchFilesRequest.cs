using System.Globalization;
using System.Text;
using System.Xml.Linq;

using FluentVault.Common.Helpers;
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
    private readonly StringBuilder _searchConditionBuilder = new();
    private object _searchValue = new();
    private long _property;
    private long _operator;
    private string _propertyName = string.Empty;
    private SearchPropertyType _propertyType = SearchPropertyType.SingleProperty;
    private IEnumerable<VaultPropertyDefinition> _allProperties = new List<VaultPropertyDefinition>();

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

    private void AddSearchCondition()
    {
        string value = _searchValue switch
        {
            string s => s,
            DateTime d => d.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
            not null => _searchValue.ToString() ?? throw new Exception("Unable to convert search value to string"),
            _ => string.Empty
        };

        string condition = GetSearchCondition(value, _property, _operator, _propertyType, SearchRule.Must);
        _searchConditionBuilder.AppendLine(condition);
        _propertyType = SearchPropertyType.SingleProperty;
    }

    private string GetInnerBody(string bookmark, string searchConditions, string? sortConditions = null, string? folderIds = null, bool recurseFolders = true, bool latestOnly = true)
    {
        StringBuilder bodyBuilder = new();
        bodyBuilder.AppendLine(GetOpeningTag());
        bodyBuilder.AppendLine("<conditions>");
        bodyBuilder.Append(searchConditions);
        bodyBuilder.AppendLine("</conditions>");

        bodyBuilder.AppendLine("<sortConditions>");
        if (string.IsNullOrWhiteSpace(sortConditions) is false)
            bodyBuilder.Append(sortConditions);
        bodyBuilder.AppendLine("</sortConditions>");

        bodyBuilder.AppendLine("<folderIds>");
        if (string.IsNullOrWhiteSpace(folderIds) is false)
            bodyBuilder.Append(folderIds);
        bodyBuilder.AppendLine("</folderIds>");

        bodyBuilder.AppendLine($"<recurseFolders>{recurseFolders.ToString().ToLower()}</recurseFolders>");
        bodyBuilder.AppendLine($"<latestOnly>{latestOnly.ToString().ToLower()}</latestOnly>");
        bodyBuilder.AppendLine($"<bookmark>{bookmark}</bookmark>");
        bodyBuilder.AppendLine(GetClosingTag());

        return bodyBuilder.ToString();
    }

    private static string GetSearchCondition(string searchText, long propertyId, long searchOperator, SearchPropertyType propertyType, SearchRule searchRule)
        => $@"<SrchCond PropDefId=""{propertyId}"" SrchOper=""{searchOperator}"" SrchTxt=""{searchText}"" PropTyp=""{propertyType}"" SrchRule=""{searchRule}""/>";

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
            string innerBody = GetInnerBody(bookmark, _searchConditionBuilder.ToString());
            string requestBody = BodyBuilder.GetRequestBody(innerBody, Session.Ticket, Session.UserId);
            XDocument document = await SendAsync(requestBody);
            VaultFileSearchResult result = document.ParseFileSearchResult();
            files.AddRange(result.Files);
            bookmark = result.Bookmark;
        } while (files.Count <= maxResultCount && string.IsNullOrEmpty(bookmark) is false);

        return files;
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
