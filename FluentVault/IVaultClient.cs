
namespace FluentVault;

internal interface IVaultClient
{
    IGetRequestBuilder Get { get; }
    ISearchRequestBuilder Search { get; }
    Guid Ticket { get; }
    IUpdateRequestBuilder Update { get; }
    long UserId { get; }
}