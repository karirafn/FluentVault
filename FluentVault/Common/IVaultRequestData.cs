namespace FluentVault.Common;

internal interface IVaultRequestData
{
    VaultRequest Get(string operation);
}
