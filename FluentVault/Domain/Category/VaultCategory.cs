namespace FluentVault;

public record VaultCategory(
    VaultCategoryId Id,
    string Name,
    string SystemName,
    long Color,
    string Description,
    IEnumerable<VaultEntityClass> EntityClasses);
