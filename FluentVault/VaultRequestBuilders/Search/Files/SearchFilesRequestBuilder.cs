﻿using System.Text;

namespace FluentVault;

internal class SearchFilesRequestBuilder : ISearchFilesRequestBuilder, ISearchFilesStringProperty, IAddSearchFilesCondition
{
    private readonly VaultSessionInfo _session;
    private readonly StringBuilder _searchConditionBuilder = new();
    private object _searchValue = new();
    private SearchOperator _operator = SearchOperator.Contains;
    private SearchPropertyType _propertyType = SearchPropertyType.SingleProperty;
    private int _property;

    public SearchFilesRequestBuilder(VaultSessionInfo session)
    {
        _session = session;
    }

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

    public IAddSearchFilesCondition InProperty(SearchStringProperty property)
    {
        _property = property.Id;
        return this;
    }

    public IAddSearchFilesCondition InAllProperties
    {
        get
        {
            _propertyType = SearchPropertyType.AllProperties;
            return this;
        }
    }

    public IAddSearchFilesCondition InAllPropertiesAndContent
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

    public async Task<VaultFile> SearchAsync()
    {
        AddSearchCondition();
        var uri = new Uri($"http://{_session.Server}/AutodeskDM/Services/v26/DocumentService.svc?op=FindFilesBySearchConditions&uid=8&vaultName={_session.Database}&sessID=382454514&app=VP");
        var body = GetSearchFilesByFilenameRequestBody(_session.Ticket, _session.UserId);
        var soapAction = @"""http://AutodeskDM/Services/Document/1/7/2020/DocumentService/FindFilesBySearchConditions""";

        var document = await VaultHttpClient.SendRequestAsync(uri, body, soapAction);
        var file = document.ParseFirstFileSearchResult();

        return file;
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

        var condition = GetSearchCondition(value, _property, _operator, _propertyType, SearchRule.Must);
        _searchConditionBuilder.AppendLine(condition);
    }

    private string GetSearchFilesByFilenameRequestBody(Guid ticket, long? userId)
    {
        var innerBody = GetSearchInnerBody(_searchConditionBuilder.ToString());
        var requestBody = BodyBuilder.GetRequestBody(innerBody, ticket, userId);

        return requestBody;
    }

    private static string GetSearchInnerBody(string searchConditions, string? sortConditions = null, string? folderIds = null, bool recurseFolders = true, bool latestOnly = true)
    {
        StringBuilder bodyBuilder = new();
        bodyBuilder.AppendLine(@"       <FindFilesBySearchConditions xmlns=""http://AutodeskDM/Services/Document/1/7/2020/"">");
        bodyBuilder.AppendLine("            <conditions>");
        bodyBuilder.Append(searchConditions);
        bodyBuilder.AppendLine("            </conditions>");

        bodyBuilder.AppendLine("            <sortConditions>");
        if (string.IsNullOrWhiteSpace(sortConditions) is false)
            bodyBuilder.Append(sortConditions);
        bodyBuilder.AppendLine("            </sortConditions>");

        bodyBuilder.AppendLine("            <folderIds>");
        if (string.IsNullOrWhiteSpace(folderIds) is false)
            bodyBuilder.Append(folderIds);
        bodyBuilder.AppendLine("            </folderIds>");

        bodyBuilder.AppendLine($"            <recurseFolders>{recurseFolders.ToString().ToLower()}</recurseFolders>");
        bodyBuilder.AppendLine($"            <latestOnly>{latestOnly.ToString().ToLower()}</latestOnly>");
        bodyBuilder.AppendLine("            <bookmark/>");
        bodyBuilder.AppendLine("        </FindFilesBySearchConditions>");

        return bodyBuilder.ToString();
    }

    private static string GetSearchCondition(string searchText, int propertyId, SearchOperator searchOperator, SearchPropertyType propertyType, SearchRule searchRule)
        => $@"              <SrchCond PropDefId=""{propertyId}"" SrchOper=""{searchOperator}"" SrchTxt=""{searchText}"" PropTyp=""{propertyType}"" SrchRule=""{searchRule}""/>";
}
