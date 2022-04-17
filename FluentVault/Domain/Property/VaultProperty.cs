namespace FluentVault;

public record VaultProperty(
    VaultPropertyDefinition Definition,
    IEnumerable<VaultPropertyConstraint> Constraints,
    IEnumerable<string> ListValues,
    IEnumerable<VaultPropertyEntityClassContentSourcePropertyDefinition> EntityClassContentSourcePropertyDefinitions);
