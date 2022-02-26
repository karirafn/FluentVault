namespace FluentVault;

public interface IGetRequestBuilder
{
    public Task<IEnumerable<VaultCategory>> Categories();
    public Task<IEnumerable<VaultLifeCycle>> Lifecycles();
    public Task<IEnumerable<VaultPropertyDefinition>> Properties();
}
