using System.Text;

namespace FluentVault;

internal class UpdateFileLifecycleStateBuilder : 
    IUpdateFileLifecycleStateBuilder,
    IWithFileMasterId,
    IWithComment
{
    private long _masterId;
    private long _stateId;
    private readonly VaultSessionInfo _session;

    public UpdateFileLifecycleStateBuilder(VaultSessionInfo session)
    {
        _session = session;
    }

    public IWithFileMasterId WithMasterId(long masterId)
    {
        _masterId = masterId;
        return this;
    }

    public IWithComment ToStateWithId(long stateId)
    {
        _stateId = stateId;
        return this;
    }

    public async Task<VaultFile> WithComment(string comment)
    {
        var uri = new Uri($"http://{_session.Server}/AutodeskDM/v26/DocumentServiceExtensions.svc?objCount=1&op=UpdateFileLifeCycleStates&uid=8&currentCommand=Connectivity.Explorer.DocumentPS.ChangeLifecycleStateCommand&vaultName={_session.Database}&app=VP");
        var body = GetUpdateFileLifecycleStateRequestBody(_masterId, _stateId, comment, _session.Ticket, _session.UserId);
        var soapAction = @"""http://AutodeskDM/Services/DocumentExtensions/1/7/2020/DocumentServiceExtensions/UpdateFileLifeCycleStates""";

        var document = await VaultHttpClient.SendRequestAsync(uri, body, soapAction);
        var file = document.ParseVaultFile();

        return file;
    }

    public async Task<VaultFile> WithoutComment() => await WithComment(string.Empty);

    private static string GetUpdateFileLifecycleStateRequestBody(long masterId, long stateId, string? comment, Guid ticket, long? userId)
    {
        var innerBody = GetUpdateFileLifecycleStateInnerBody(masterId, stateId, comment);
        var requestBody = BodyBuilder.GetRequestBody(innerBody, ticket, userId);

        return requestBody;
    }

    private static string GetUpdateFileLifecycleStateInnerBody(long masterId, long stateId, string? comment)
    {
        StringBuilder bodyBuilder = new();
        bodyBuilder.AppendLine(@"       <UpdateFileLifeCycleStates xmlns=""http://AutodeskDM/Services/DocumentExtensions/1/7/2020/"">");
        bodyBuilder.AppendLine("            <fileMasterIds>");
        bodyBuilder.AppendLine($"               <long>{masterId}</long>");
        bodyBuilder.AppendLine("            </fileMasterIds>");
        bodyBuilder.AppendLine("            <toStateIds>");
        bodyBuilder.AppendLine($"               <long>{stateId}</long>");
        bodyBuilder.AppendLine("            </toStateIds>");
        bodyBuilder.AppendLine($"           <comment>{comment}</comment>");
        bodyBuilder.AppendLine("        </UpdateFileLifeCycleStates>");

        return bodyBuilder.ToString();
    }
}
