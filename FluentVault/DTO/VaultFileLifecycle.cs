namespace FluentVault;

public record VaultFileLifecycle(
    long StateId,
    long DefinitionId,
    string StateName,
    bool IsConsume,
    bool IsObsolete);
