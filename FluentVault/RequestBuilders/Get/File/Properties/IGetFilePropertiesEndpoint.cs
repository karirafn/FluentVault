namespace FluentVault;

public interface IGetFilePropertiesEndpoint
{
    public Task<IEnumerable<VaultPropertyInstance>> ExecuteAsync(CancellationToken cancellationToken = default);
}
