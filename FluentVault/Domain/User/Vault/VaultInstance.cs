namespace FluentVault;
public record VaultInstance(
    VaultInstanceId Id,
    string Name,
    DateTime CreateDate,
    VaultUserId CreateUserId);
