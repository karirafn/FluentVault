using System.Text;

namespace FluentVault;

internal class SearchFilesRequestBuilder : ISearchFilesRequestBuilder
{
    private readonly VaultSessionInfo _session;

    public SearchFilesRequestBuilder(VaultSessionInfo session)
    {
        _session = session;
    }

    public async Task<VaultFile> ByFilename(string filename)
    {
        var uri = new Uri($"http://{_session.Server}/AutodeskDM/Services/v26/DocumentService.svc?op=FindFilesBySearchConditions&uid=8&vaultName={_session.Database}&sessID=382454514&app=VP");
        var body = GetSearchFilesByFilenameRequestBody(filename, _session.Ticket, _session.UserId);
        var soapAction = @"""http://AutodeskDM/Services/Document/1/7/2020/DocumentService/FindFilesBySearchConditions""";

        var document = await VaultHttpClient.SendRequestAsync(uri, body, soapAction);
        var file = document.ParseFirstFileSearchResult();

        return file;
    }

    private static string GetSearchFilesByFilenameRequestBody(string filename, Guid ticket, long? userId)
    {
        var filenameSearchCondition = GetSearchCondition(filename, VaultProperty.Filename, SearchOperator.Contains, SearchPropertyType.SingleProperty, SearchRule.Must);
        var innerBody = GetSearchInnerBody(filenameSearchCondition);
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

    private static string GetSearchCondition(string searchText, VaultProperty property, SearchOperator searchOperator, SearchPropertyType propertyType, SearchRule searchRule)
        => $@"              <SrchCond PropDefId=""{property}"" SrchOper=""{searchOperator}"" SrchTxt=""{searchText}"" PropTyp=""{propertyType}"" SrchRule=""{searchRule}""/>";
}
