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
    string RestrictPurgeOption,
    string ItemFileSecurityMode,
    string FolderFileSecurityMode,
    IEnumerable<string> CommentArray);
