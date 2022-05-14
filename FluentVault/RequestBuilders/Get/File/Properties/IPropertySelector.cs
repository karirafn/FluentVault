namespace FluentVault;

public interface IPropertySelector
{
    IGetFilePropertiesEndpoint AndProperty(VaultPropertyDefinitionId id);
    IGetFilePropertiesEndpoint AndProperties(IEnumerable<VaultPropertyDefinitionId> ids);
}
