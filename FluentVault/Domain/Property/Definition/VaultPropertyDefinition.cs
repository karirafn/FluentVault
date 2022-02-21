namespace FluentVault;

public record VaultPropertyDefinition(
    PropertyDefinition Definition,
    IEnumerable<PropertyConstraint> Constraints,
    IEnumerable<string> ListValues,
    IEnumerable<EntityClassContentSourcePropertyDefinition> EntityClassContentSourcePropertyDefinitions);
