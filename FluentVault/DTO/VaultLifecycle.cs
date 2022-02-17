namespace FluentVault;

public record VaultLifecycle(
    long Id,
    string Name,
    string SystemName,
    string DisplayName,
    string Description,
    string SecurityDefinition,
    IEnumerable<VaultLifecycleState> States,
    IEnumerable<VaultLifecycleStateTransition> Transitions);
