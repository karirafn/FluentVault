namespace FluentVault;

public record VaultSessionCredentials(Guid Ticket = default, long UserId = default);
