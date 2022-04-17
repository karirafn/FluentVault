using FluentVault.RequestBuilders;

namespace FluentVault.Requests.Search;

internal class SearchRequestBuilder : IRequestBuilder, ISearchRequestBuilder
{
    private readonly ISearchFilesRequestBuilder _files;

    public SearchRequestBuilder(ISearchFilesRequestBuilder files)
    {
        _files = files;
    }

    public ISearchFilesRequestBuilder Files => _files;
}
