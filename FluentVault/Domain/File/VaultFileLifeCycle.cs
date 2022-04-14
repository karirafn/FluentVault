namespace FluentVault;

public record VaultFileLifeCycle(
    VaultLifeCycleStateId StateId,
    VaultLifeCycleDefinitionId DefinitionId,
    string StateName,
    bool IsReleased,
    bool IsObsolete);
