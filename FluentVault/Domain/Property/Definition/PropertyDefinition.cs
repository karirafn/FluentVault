namespace FluentVault;

public record PropertyDefinition(
    long Id,
    DataType DataType,
    string DisplayName,
    string SystemName,
    bool IsActive,
    bool IsUsedInBasicSearch,
    bool IsSystemProperty,
    long UsageCount,
    IEnumerable<EntityClassAssociation> EntityClassAssociations);
