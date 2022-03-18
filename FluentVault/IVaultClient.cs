namespace FluentVault;

public interface IVaultClient
{
    IGetRequestBuilder Get { get; }
    ISearchRequestBuilder Search { get; }
    IUpdateRequestBuilder Update { get; }
}
