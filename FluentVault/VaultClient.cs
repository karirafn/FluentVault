namespace FluentVault;

internal class VaultClient : IVaultClient
{
    private readonly IGetRequestBuilder _get;
    private readonly ISearchRequestBuilder _search;
    private readonly IUpdateRequestBuilder _update;

    public VaultClient(IGetRequestBuilder get, ISearchRequestBuilder search, IUpdateRequestBuilder update)
    {
        _get = get;
        _search = search;
        _update = update;
    }

    public IGetRequestBuilder Get => _get;
    public ISearchRequestBuilder Search => _search;
    public IUpdateRequestBuilder Update => _update;
}
