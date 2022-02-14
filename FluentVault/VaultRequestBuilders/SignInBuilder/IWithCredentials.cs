namespace FluentVault;

public interface IWithCredentials
{
    public Task<Vault> WithCredentials(string username, string password);
}
