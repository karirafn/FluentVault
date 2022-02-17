namespace FluentVault;

public record VaultLifecycleState(
    long Id,
    string Name,
    string DisplayName,
    string Description,
    bool IsDefault,
    long LifecycleId,
    bool HasStateBasedSecurity,
    bool IsReleasedState,
    bool IsObsoleteState,
    long DisplayOrder,
    RestrictPurgeOption RestrictPurgeOption,
    ItemToFileSecurityMode ItemFileSecurityMode,
    FolderFileSecurityMode FolderFileSecurityMode,
    IEnumerable<string> Comments);
