namespace FluentVault;

internal class GetRequestBuilder : IGetRequestBuilder
{
    private readonly VaultSessionInfo _session;

    public GetRequestBuilder(VaultSessionInfo session)
    {
        _session = session;
    }

    public async Task<IEnumerable<VaultLifecycle>> Lifecycles()
    {
        var innerBody = @"<GetAllLifeCycleDefinitions xmlns=""http://AutodeskDM/Services/LifeCycle/1/7/2020/""/>";
        var body = BodyBuilder.GetRequestBody(innerBody, _session.Ticket, _session.UserId);
        Uri uri = new($"http://{_session.Server}/AutodeskDM/Services/v26/LifeCycleService.svc?op=GetAllLifeCycleDefinitions&uid=8&currentCommand=Connectivity.Explorer.Admin.AdminToolsCommand&vaultName={_session.Database}&app=VP");
        var soapAction = @"""http://AutodeskDM/Services/LifeCycle/1/7/2020/LifeCycleService/GetAllLifeCycleDefinitions""";

        var document = await VaultHttpClient.SendRequestAsync(uri, body, soapAction);
        var lifecycles = document.ParseLifecycles();

        return lifecycles;
    }
}
