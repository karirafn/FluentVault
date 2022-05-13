using FluentVault.RequestBuilders;

namespace FluentVault;
public interface ISearchRequestBuilder : IRequestBuilder
{
    public ISearchFilesRequestBuilder Files { get; }
    public ISearchItemsRequestBuilder Items { get; }
}
