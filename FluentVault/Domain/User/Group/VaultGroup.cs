namespace FluentVault;
public record VaultGroup(
    VaultGroupId Id,
    string Name,
    string Email,
    VaultUserId CreateUserId,
    DateTime CreateDate,
    bool IsActive,
    bool IsSystemGroup,
    VaultAuthenticationType AuthenticationType);
