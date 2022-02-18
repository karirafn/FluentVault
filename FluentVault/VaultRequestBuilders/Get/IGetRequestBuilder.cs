namespace FluentVault;

public interface IGetRequestBuilder
{
    public Task<IEnumerable<VaultLifecycle>> Lifecycles();
}
