namespace FluentVault.Common;

internal interface IVaultRequestDataCollection
{
    VaultRequestData Get(string operation);
}
