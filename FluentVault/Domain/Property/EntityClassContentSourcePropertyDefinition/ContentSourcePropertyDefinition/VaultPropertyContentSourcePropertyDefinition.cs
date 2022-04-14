namespace FluentVault;

public record VaultPropertyContentSourcePropertyDefinition(
    VaultPropertyContentSourceId ContentSourceId,
    string DisplayName,
    string Moniker,
    VaultPropertyAllowedMappingDirection MappingDirection,
    bool CanCreateNew,
    VaultPropertyClassification Classification,
    VaultDataType DataType,
    VaultPropertyContentSourceDefinitionType ContentSourceDefinitionType);
