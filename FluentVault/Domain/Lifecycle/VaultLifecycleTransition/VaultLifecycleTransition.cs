namespace FluentVault;

public record VaultLifeCycleTransition(
    long Id,
    long FromId,
    long ToId,
    BumpRevisionState BumpRevision,
    SynchronizePropertiesState SynchronizeProperties,
    EnforceChildState EnforceChildState,
    EnforceContentState EnforceContentState,
    FileLinkTypeState ItemFileLnkUptodate,
    FileLinkTypeState ItemFileLnkState,
    bool VerifyThatChildIsNotObsolete,
    bool TransitionBasedSecurity,
    bool UpdateItems);
