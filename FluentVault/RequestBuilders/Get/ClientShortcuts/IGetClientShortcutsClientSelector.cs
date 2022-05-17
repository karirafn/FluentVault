namespace FluentVault;

public interface IGetClientShortcutsClientSelector
{
    public IGetClientShortcutsEntitySelector WithClientType(VaultClientType clientType);
}
