using FluentVault.Requests.Search.Files;

namespace FluentVault.Requests.Search;

internal class SearchRequest : ISearchRequest
{
    private readonly VaultSession _session;

    public SearchRequest(VaultSession session)
    {
        _session = session;
    }

    public ISearchFilesRequestBuilder Files => new SearchFilesRequest(_session);
}
