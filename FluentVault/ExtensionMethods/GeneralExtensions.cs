namespace FluentVault;

internal static class GeneralExtensions
{
    internal static VaultHttpRequestMessage CreateVaultHttpRequestMessage(this VaultStringContent content, Uri uri, string soapAction)
        => new(uri, content, soapAction);
}
