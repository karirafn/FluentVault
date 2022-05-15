namespace FluentVault;
public interface IEntitySelector
{
    IPropertySelector WithId(VaultEntityId id);
    IPropertySelector WithIds(IEnumerable<VaultEntityId> ids);
}
