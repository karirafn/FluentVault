namespace FluentVault;

public record VaultSession(string Server, string Database, Guid Ticket, long UserId);
