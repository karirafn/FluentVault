namespace FluentVault;

public interface IUpdateFilePropertDefinitionsAction
{
    public IUpdateFilePropertDefinitionsAction AddPropertiesByIds(IEnumerable<long> ids);
    public IUpdateFilePropertDefinitionsAction AddPropertyById(long id);
    public IUpdateFilePropertDefinitionsAction AddPropertiesByNames(IEnumerable<string> names);
    public IUpdateFilePropertDefinitionsAction AddPropertyByName(string name);
    public IUpdateFilePropertDefinitionsAction RemovePropertiesByIds(IEnumerable<long> ids);
    public IUpdateFilePropertDefinitionsAction RemovePropertyById(long id);
    public IUpdateFilePropertDefinitionsAction RemovePropertiesByNames(IEnumerable<string> names);
    public IUpdateFilePropertDefinitionsAction RemovePropertyByName(string name);
    public Task<IEnumerable<VaultFile>> ExecuteAsync();
}
