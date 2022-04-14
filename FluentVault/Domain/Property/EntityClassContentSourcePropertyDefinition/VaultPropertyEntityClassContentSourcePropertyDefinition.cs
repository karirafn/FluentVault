namespace FluentVault;

public record VaultPropertyEntityClassContentSourcePropertyDefinition(
    VaultEntityClass EntityClass,
    IEnumerable<VaultPropertyContentSourcePropertyDefinition> ContentSourcePropertyDefinitions,
    IEnumerable<VaultPropertyMappingType> MappingTypes,
    IEnumerable<long> Prioroties,
    IEnumerable<VaultPropertyMappingDirection> MappingDirections,
    IEnumerable<bool> CanCreateNew);
