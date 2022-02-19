namespace FluentVault;

public record ContentSourcePropertyDefinition(
    long ContentSourceId,
    string DisplayName,
    string Moniker,
    AllowedMappingDirection MappingDirection,
    bool CanCreateNew,
    Classification Classification,
    DataType DataType,
    ContentSourceDefinitionType ContentSourceDefinitionType);
