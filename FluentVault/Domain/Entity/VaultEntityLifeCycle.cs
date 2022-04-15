namespace FluentVault;

public record VaultEntityLifeCycle(
    VaultLifeCycleStateId StateId,
    VaultLifeCycleDefinitionId DefinitionId,
    string StateName,
    bool IsReleased,
    bool IsObsolete);
