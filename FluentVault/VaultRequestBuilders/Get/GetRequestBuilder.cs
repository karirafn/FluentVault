namespace FluentVault;

internal class GetRequestBuilder : IGetRequestBuilder
{
    private readonly VaultSessionInfo _session;
    private readonly LifecycleService _lifecycleService;

    public GetRequestBuilder(VaultSessionInfo session)
    {
        _session = session;
        _lifecycleService = new(_session);
    }

    public async Task<IEnumerable<VaultLifecycle>> Lifecycles() => await _lifecycleService.GetAll();
}
