namespace FluentVault;

internal class GetRequestBuilder : IGetRequestBuilder, IGetFileRequestBuilder
{
    private readonly VaultSessionInfo _session;

    public GetRequestBuilder(VaultSessionInfo session)
    {
        _session = session;
    }

    public IGetFileRequestBuilder File => this;
    public IGetFileMasterIdRequestBuilder MasterId => new GetFileMasterIdRequestBuilder(_session);
}
