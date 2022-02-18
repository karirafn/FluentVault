namespace FluentVault.Domain.Properties;

public record VaultProperty(
    long Id,
    DataType DataType,
    string DisplayName,
    string SystemName,
    bool IsActive,
    bool IsUsedInBasicSearch,
    bool IsSystemProperty,
    long UsageCount);
