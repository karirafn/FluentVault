namespace FluentVault;

public record EntityClassContentSourcePropertyDefinition(
    EntityClass EntityClass,
    IEnumerable<ContentSourcePropertyDefinition> ContentSourcePropertyDefinitions,
    IEnumerable<MappingType> MappingTypes,
    IEnumerable<long> Prioroties,
    IEnumerable<MappingDirection> MappingDirections,
    IEnumerable<bool> CanCreateNew);
