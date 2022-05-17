namespace FluentVault;

public interface IGetClientShortcutsEndpoint
{
    public Task<IEnumerable<Uri>> ExecuteAsync(CancellationToken cancellationToken = default);
}
