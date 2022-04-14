namespace FluentVault;

public record VaultLifeCycleDefinition(
    long Id,
    string Name,
    string SystemName,
    string DisplayName,
    string Description,
    string SecurityDefinition,
    IEnumerable<VaultLifeCycleState> States,
    IEnumerable<VaultLifeCycleTransition> Transitions);
