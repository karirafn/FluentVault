namespace FluentVault.Domain;

public record VaultSessionCredentials(Guid Ticket = default, long UserId = default);
