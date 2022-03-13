namespace FluentVault;

public interface IGetRequestBuilder
{
    public Task<IEnumerable<VaultCategory>> CategoryConfigurations();
    public Task<IEnumerable<VaultLifeCycleDefinition>> LifeCycleDefinitions();
    public Task<IEnumerable<VaultProperty>> PropertyDefinitionInfos();
}
