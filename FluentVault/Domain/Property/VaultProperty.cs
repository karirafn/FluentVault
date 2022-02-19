namespace FluentVault;

public record VaultProperty(
    PropertyDefinition Definition,
    IEnumerable<PropertyConstraint> Constraints,
    IEnumerable<string> ListValues,
    IEnumerable<EntityClassContentSourcePropertyDefinition> EntityClassContentSourcePropertyDefinitions);
