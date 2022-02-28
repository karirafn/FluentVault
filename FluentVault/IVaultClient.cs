using FluentVault.Domain;

namespace FluentVault;

internal interface IVaultClient
{
    Task<VaultSessionCredentials> SignIn();
    IGetRequestBuilder Get { get; }
    ISearchRequestBuilder Search { get; }
    IUpdateRequestBuilder Update { get; }
}
