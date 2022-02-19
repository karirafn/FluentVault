namespace FluentVault;

public record PropertyConstraint(
    long Id,
    long PropertyDefinitionId,
    long CategoryId,
    PropertyConstraintType Type,
    bool Value);
