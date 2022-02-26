namespace FluentVault;

public record VaultLifeCycle(
    long Id,
    string Name,
    string SystemName,
    string DisplayName,
    string Description,
    string SecurityDefinition,
    IEnumerable<VaultLifeCycleState> States,
    IEnumerable<VaultLifeCycleTransition> Transitions);
