namespace FluentVault;

public interface IGetClientShortcutEndpoint
{
    public Task<Uri> ExecuteAsync(CancellationToken cancellationToken = default);
}
