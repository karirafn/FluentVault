namespace FluentVault;

public interface IGetFilePropertiesRequestBuilder
{
    IPropertySelector ByFileId(VaultEntityId id);
    IPropertySelector ByFileIds(IEnumerable<VaultEntityId> ids);
}
