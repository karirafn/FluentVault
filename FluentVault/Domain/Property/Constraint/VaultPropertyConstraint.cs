namespace FluentVault;

public record VaultPropertyConstraint(
    VaultPropertyConstraintId Id,
    VaultPropertyDefinitionId PropertyDefinitionId,
    VaultCategoryId CategoryId,
    VaultPropertyConstraintType Type,
    bool Value);
