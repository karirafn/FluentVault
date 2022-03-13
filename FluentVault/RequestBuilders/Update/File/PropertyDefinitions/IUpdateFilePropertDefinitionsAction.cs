namespace FluentVault;

public interface IUpdateFilePropertDefinitionsAction
{
    public IUpdateFilePropertDefinitionsAction AddPropertiesByIds(IEnumerable<VaultPropertyDefinitionId> ids);
    public IUpdateFilePropertDefinitionsAction AddPropertyById(VaultPropertyDefinitionId id);
    public IUpdateFilePropertDefinitionsAction AddPropertiesByNames(IEnumerable<string> names);
    public IUpdateFilePropertDefinitionsAction AddPropertyByName(string name);
    public IUpdateFilePropertDefinitionsAction RemovePropertiesByIds(IEnumerable<VaultPropertyDefinitionId> ids);
    public IUpdateFilePropertDefinitionsAction RemovePropertyById(VaultPropertyDefinitionId id);
    public IUpdateFilePropertDefinitionsAction RemovePropertiesByNames(IEnumerable<string> names);
    public IUpdateFilePropertDefinitionsAction RemovePropertyByName(string name);
    public Task<IEnumerable<VaultFile>> ExecuteAsync();
}
