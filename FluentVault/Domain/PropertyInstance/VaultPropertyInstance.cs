namespace FluentVault;
public record VaultPropertyInstance(VaultEntityId EntityId, VaultPropertyDefinitionId PropertyId, VaultDataType ValueType, string? Value);
