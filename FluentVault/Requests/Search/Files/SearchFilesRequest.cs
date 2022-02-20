using System.Globalization;
using System.Text;
using System.Xml.Linq;

using FluentVault.Common.Helpers;
using FluentVault.Domain.File;
using FluentVault.Domain.Search;
using FluentVault.Requests.Get.Properties;

namespace FluentVault.Requests.Search.Files;

internal class SearchFilesRequest : SessionRequest,
    ISearchFilesRequest,
    ISearchFilesStringProperty,
    ISearchFilesDateTimeProperty,
    ISearchFilesAddSearchCondition
{
    private readonly StringBuilder _searchConditionBuilder = new();
    private object _searchValue = new();
    private long _operator;
    private long _property;
    private string _propertyName = string.Empty;
    private SearchPropertyType _propertyType = SearchPropertyType.SingleProperty;
    private IEnumerable<VaultProperty> _allProperties = new List<VaultProperty>();

    public SearchFilesRequest(VaultSession session)
        : base(session, RequestData.FindFilesBySearchConditions) { }

    public async Task<IEnumerable<VaultFile>> SearchAllAsync()
    {
        if (string.IsNullOrEmpty(_propertyName) == false)
            await SetPropertyValue(_propertyName);

        AddSearchCondition();

        string innerBody = GetSearchInnerBody(_searchConditionBuilder.ToString());
        string requestBody = BodyBuilder.GetRequestBody(innerBody, Session.Ticket, Session.UserId);

        XDocument document = await SendAsync(requestBody);
        IEnumerable<VaultFile> files = document.ParseAllVaultFiles();

        return files;
    }

    public async Task<VaultFile?> SearchSingleAsync()
    {
        IEnumerable<VaultFile> files = await SearchAllAsync();

        return files.FirstOrDefault();
    }

    public ISearchFilesStringProperty ForValueEqualTo(string value)
    {
        _searchValue = value;
        _operator = (long)SearchOperator.IsEqualTo;
        return this;
    }

    public ISearchFilesStringProperty ForValueContaining(string value)
    {
        _searchValue = value;
        _operator = (long)SearchOperator.Contains;
        return this;
    }

    public ISearchFilesStringProperty ForValueNotContaining(string value)
    {
        _searchValue = value;
        _operator = (long)SearchOperator.DoesNotContain;
        return this;
    }

    public ISearchFilesDateTimeProperty ForValueEqualTo(DateTime value)
    {
        _searchValue = value;
        _operator = (long)SearchOperator.IsEqualTo;
        return this;
    }

    public ISearchFilesDateTimeProperty ForValueNotEqualTo(DateTime value)
    {
        _searchValue = value;
        _operator = (long)SearchOperator.IsNotEqualTo;
        return this;
    }

    public ISearchFilesDateTimeProperty ForValueLessThan(DateTime value)
    {
        _searchValue = value;
        _operator = (long)SearchOperator.IsLessThan;
        return this;
    }

    public ISearchFilesDateTimeProperty ForValueLessThanOrEqualTo(DateTime value)
    {
        _searchValue = value;
        _operator = (long)SearchOperator.IsLessThanOrEqualTo;
        return this;
    }

    public ISearchFilesDateTimeProperty ForValueGreaterThan(DateTime value)
    {
        _searchValue = value;
        _operator = (long)SearchOperator.IsGreaterThan;
        return this;
    }

    public ISearchFilesDateTimeProperty ForValueGreaterThanOrEqualTo(DateTime value)
    {
        _searchValue = value;
        _operator = (long)SearchOperator.IsGreaterThanOrEqualTo;
        return this;
    }

    public ISearchFilesAddSearchCondition InUserProperty(string name)
    {
        _propertyName = name;
        return this;
    }

    public ISearchFilesAddSearchCondition InSystemProperty(SearchDateTimeProperty property)
    {
        _property = (long)property;
        return this;
    }

    public ISearchFilesAddSearchCondition InSystemProperty(SearchStringProperty property)
    {
        _property = (long)property;
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
            long l => l.ToString(),
            DateTime d => d.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
            _ => string.Empty
        };

        string condition = GetSearchCondition(value, _property, (long)_operator, _propertyType, SearchRule.Must);
        _searchConditionBuilder.AppendLine(condition);
    }

    private string GetSearchInnerBody(string searchConditions, string? sortConditions = null, string? folderIds = null, bool recurseFolders = true, bool latestOnly = true)
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
        bodyBuilder.AppendLine("<bookmark/>");
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
}
