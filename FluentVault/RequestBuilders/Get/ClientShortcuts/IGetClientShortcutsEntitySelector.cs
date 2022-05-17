namespace FluentVault;

public interface IGetClientShortcutsEntitySelector
{
    public IGetClientShortcutsEndpoint WithMasterId(VaultMasterId masterId);
    public IGetClientShortcutsEndpoint WithMasterIds(IEnumerable<VaultMasterId> masterIds);
}
