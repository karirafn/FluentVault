namespace FluentVault;

public record VaultFileLifecycle(
    long StateId,
    long DefinitionId,
    string StateName,
    bool IsReleased,
    bool IsObsolete);
