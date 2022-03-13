namespace FluentVault;

public interface IUpdateFilePropertDefinitionsAction
{
    public IUpdateFilePropertDefinitionsAction AddPropertiesByIds(IEnumerable<VaultPropertyId> ids);
    public IUpdateFilePropertDefinitionsAction AddPropertyById(VaultPropertyId id);
    public IUpdateFilePropertDefinitionsAction AddPropertiesByNames(IEnumerable<string> names);
    public IUpdateFilePropertDefinitionsAction AddPropertyByName(string name);
    public IUpdateFilePropertDefinitionsAction RemovePropertiesByIds(IEnumerable<VaultPropertyId> ids);
    public IUpdateFilePropertDefinitionsAction RemovePropertyById(VaultPropertyId id);
    public IUpdateFilePropertDefinitionsAction RemovePropertiesByNames(IEnumerable<string> names);
    public IUpdateFilePropertDefinitionsAction RemovePropertyByName(string name);
    public Task<IEnumerable<VaultFile>> ExecuteAsync();
}
