namespace FluentVault;
public record VaultEntityLifeCycle(
    VaultLifeCycleStateId StateId,
    VaultLifeCycleDefinitionId DefinitionId,
    bool IsReleased,
    bool IsObsolete);
