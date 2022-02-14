namespace FluentVault;

public interface ISignOutRequestBuilder
{
    public IWithSessionCredentials FromVault(string server, string database);
}
