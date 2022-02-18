namespace FluentVault;

public interface IGetRequest
{
    public Task<IEnumerable<VaultCategory>> Categories();
    public Task<IEnumerable<VaultLifecycle>> Lifecycles();
}
