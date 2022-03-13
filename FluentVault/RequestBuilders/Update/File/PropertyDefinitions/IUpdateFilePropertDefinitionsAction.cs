namespace FluentVault;

public interface IUpdateFilePropertDefinitionsAction
{
    public IUpdateFilePropertDefinitionsAction AddPropertiesByIds(IEnumerable<PropertyId> ids);
    public IUpdateFilePropertDefinitionsAction AddPropertyById(PropertyId id);
    public IUpdateFilePropertDefinitionsAction AddPropertiesByNames(IEnumerable<string> names);
    public IUpdateFilePropertDefinitionsAction AddPropertyByName(string name);
    public IUpdateFilePropertDefinitionsAction RemovePropertiesByIds(IEnumerable<PropertyId> ids);
    public IUpdateFilePropertDefinitionsAction RemovePropertyById(PropertyId id);
    public IUpdateFilePropertDefinitionsAction RemovePropertiesByNames(IEnumerable<string> names);
    public IUpdateFilePropertDefinitionsAction RemovePropertyByName(string name);
    public Task<IEnumerable<VaultFile>> ExecuteAsync();
}
