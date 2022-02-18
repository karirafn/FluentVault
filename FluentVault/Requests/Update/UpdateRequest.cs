namespace FluentVault;

internal class UpdateRequest : IUpdateRequest, IUpdateFileRequest
{
    private readonly VaultSession _session;

    public UpdateRequest(VaultSession session)
    {
        _session = session;
    }

    public IUpdateFileRequest File => this;
    public IUpdateFileLifecycleStateRequest LifecycleState => new UpdateFileLifecycleStateRequest(_session);
}
