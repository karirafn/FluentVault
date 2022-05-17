namespace FluentVault;
public interface IGetPropertiesEntitySelector
{
    IPropertySelector WithId(VaultEntityId id);
    IPropertySelector WithIds(IEnumerable<VaultEntityId> ids);
}
