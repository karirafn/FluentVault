namespace FluentVault;

public interface IWithSessionCredentials
{
    public Task WithSessionCredentials(Guid ticket, long userId);
}
