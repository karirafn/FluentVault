namespace FluentVault;

internal class UpdateRequestBuilder : IUpdateRequestBuilder, IUpdateFileRequestBuilder
{
    private readonly VaultSessionInfo _session;

    public UpdateRequestBuilder(VaultSessionInfo session)
    {
        _session = session;
    }

    public IUpdateFileRequestBuilder File => this;
    public IUpdateFileLifecycleStateBuilder LifecycleState => new UpdateFileLifecycleStateBuilder(_session);
}
