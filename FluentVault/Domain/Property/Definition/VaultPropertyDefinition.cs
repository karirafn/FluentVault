namespace FluentVault;

public record VaultPropertyDefinition(
    VaultPropertyDefinitionId Id,
    VaultDataType DataType,
    string DisplayName,
    string SystemName,
    bool IsActive,
    bool IsUsedInBasicSearch,
    bool IsSystemProperty,
    long UsageCount,
    IEnumerable<VaultPropertyEntityClassAssociation> EntityClassAssociations);
