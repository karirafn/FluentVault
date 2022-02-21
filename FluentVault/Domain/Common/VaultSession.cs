namespace FluentVault.Domain.Common;

internal record VaultSession(string Server, string Database, Guid Ticket, long UserId);
