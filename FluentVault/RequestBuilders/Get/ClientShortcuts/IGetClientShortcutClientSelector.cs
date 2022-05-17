namespace FluentVault;

public interface IGetClientShortcutClientSelector
{
    public IGetClientShortcutEntitySelector WithClientType(VaultClientType clientType);
}
