namespace FluentVault;

public record VaultSessionCredentials(Guid Ticket = default, long UserId = default)
{
    public bool IsValid = Ticket != default && UserId > 0;
}
