namespace FluentVault;

public interface IGetPropertiesEndpoint
{
    public Task<IEnumerable<VaultPropertyInstance>> ExecuteAsync(CancellationToken cancellationToken = default);
}
