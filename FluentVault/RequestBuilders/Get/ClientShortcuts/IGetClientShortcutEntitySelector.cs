namespace FluentVault;

public interface IGetClientShortcutEntitySelector
{
    public IGetClientShortcutEndpoint WithMasterId(VaultMasterId masterId);
}
