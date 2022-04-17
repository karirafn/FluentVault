namespace FluentVault;

public record VaultLifeCycleState(
    VaultLifeCycleStateId Id,
    string Name,
    string DisplayName,
    string Description,
    bool IsDefault,
    VaultLifeCycleDefinitionId LifecycleId,
    bool HasStateBasedSecurity,
    bool IsReleasedState,
    bool IsObsoleteState,
    long DisplayOrder,
    VaultRestrictPurgeOption RestrictPurgeOption,
    VaultItemToFileSecurityMode ItemFileSecurityMode,
    VaultFolderFileSecurityMode FolderFileSecurityMode,
    IEnumerable<string> Comments);
