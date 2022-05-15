namespace FluentVault;

public interface IPropertySelector
{
    IGetPropertiesEndpoint WithProperty(VaultPropertyDefinitionId id);
    IGetPropertiesEndpoint WithProperties(IEnumerable<VaultPropertyDefinitionId> ids);
}
