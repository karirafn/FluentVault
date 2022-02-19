using System.Text;
using System.Xml.Linq;

namespace FluentVault;

internal class SearchFilesRequest : SessionRequest, ISearchFilesRequestBuilder, ISearchFilesStringProperty, ISearchFilesAddSearchCondition
{
    private readonly StringBuilder _searchConditionBuilder = new();
    private object _searchValue = new();
    private SearchOperator _operator = SearchOperator.Contains;
    private SearchPropertyType _propertyType = SearchPropertyType.SingleProperty;
    private long _property;

    public SearchFilesRequest(VaultSession session) : base(session, "FindFilesBySearchConditions") { }

    public ISearchFilesStringProperty ForValueContaining(string value)
    {
        _searchValue = value;
        _operator = SearchOperator.Contains;
        return this;
    }

    public ISearchFilesStringProperty ForValueNotContaining(string value)
    {
        _searchValue = value;
        _operator = SearchOperator.DoesNotContain;
        return this;
    }

    public ISearchFilesAddSearchCondition InProperty(SearchStringProperty property)
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

    public ISearchFilesRequestBuilder And
    {
        get
        {
            AddSearchCondition();
            return this;
        }
    }

    public async Task<IEnumerable<VaultFile>> SearchAllAsync()
    {
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

    private void AddSearchCondition()
    {
        string value = _searchValue switch
        {
            string s => s,
            long l => l.ToString(),
            DateTime d => d.ToString(),
            _ => string.Empty
        };

        string condition = GetSearchCondition(value, _property, _operator, _propertyType, SearchRule.Must);
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

    private static string GetSearchCondition(string searchText, long propertyId, SearchOperator searchOperator, SearchPropertyType propertyType, SearchRule searchRule)
        => $@"<SrchCond PropDefId=""{propertyId}"" SrchOper=""{searchOperator}"" SrchTxt=""{searchText}"" PropTyp=""{propertyType}"" SrchRule=""{searchRule}""/>";
}
