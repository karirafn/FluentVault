namespace FluentVault;

public interface ISignInRequestBuilder
{
    public IWithCredentials ToVault(string server, string database);
}
