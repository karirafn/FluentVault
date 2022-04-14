namespace FluentVault;
public record VaultRole(
    VaultRoleId Id,
    string Name,
    string SystemName,
    bool IsSystemRole,
    string Description);
