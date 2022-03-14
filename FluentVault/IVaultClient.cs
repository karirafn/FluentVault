namespace FluentVault;

public interface IVaultClient
{
    Task<VaultSessionCredentials> SignIn();
    IGetRequestBuilder Get { get; }
    ISearchRequestBuilder Search { get; }
    IUpdateRequestBuilder Update { get; }
}
