namespace FluentVault;

public record VaultSessionInfo(string Server, string Database, Guid Ticket, long UserId);
