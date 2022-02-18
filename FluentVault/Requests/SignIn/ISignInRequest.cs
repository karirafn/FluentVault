namespace FluentVault;

public interface ISignInRequest
{
    public IWithCredentials ToVault(string server, string database);
}
