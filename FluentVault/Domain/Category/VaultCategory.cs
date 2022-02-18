namespace FluentVault;

public record VaultCategory(
    long Id,
    string Name,
    string SystemName,
    long Color,
    string Description,
    IEnumerable<EntityClass> EntityClasses);
