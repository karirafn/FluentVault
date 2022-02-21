using FluentVault.Domain.Common;
using FluentVault.Requests.Search.Files;

namespace FluentVault.Requests.Search;

internal class SearchRequest : ISearchRequest
{
    private readonly VaultSession _session;

    public SearchRequest(VaultSession session)
    {
        _session = session;
    }

    public ISearchFilesRequest Files => new SearchFilesRequest(_session);
}
