namespace FluentVault;

public record VaultUser(
    VaultUserId Id,
    string Name,
    string FirstName,
    string LastName,
    string Email,
    VaultUserId CreateUserId,
    DateTime CreateDate,
    bool IsActive,
    bool IsSystemUser,
    VaultAuthenticationType AuthenticationType);
