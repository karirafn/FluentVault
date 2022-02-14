namespace FluentVault;

internal class SearchRequestBuilder : ISearchRequestBuilder
{
    private readonly VaultSessionInfo _session;

    public SearchRequestBuilder(VaultSessionInfo session)
    {
        _session = session;
    }

    public ISearchFilesRequestBuilder Files => new SearchFilesRequestBuilder(_session);
}
