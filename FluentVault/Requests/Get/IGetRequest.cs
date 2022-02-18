namespace FluentVault;

public interface IGetRequest
{
    public Task<IEnumerable<VaultLifecycle>> Lifecycles();
}
