using FluentVault.RequestBuilders;

namespace FluentVault.Requests.Search;

internal class SearchRequestBuilder : IRequestBuilder, ISearchRequestBuilder
{
    public SearchRequestBuilder(ISearchFilesRequestBuilder files, ISearchItemsRequestBuilder items)
    {
        Files = files;
        Items = items;
    }

    public ISearchFilesRequestBuilder Files { get; }
    public ISearchItemsRequestBuilder Items { get; }
}
