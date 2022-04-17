namespace FluentVault;

public record VaultLifeCycleTransition(
    VaultLifeCycleTransitionId Id,
    VaultLifeCycleStateId FromId,
    VaultLifeCycleStateId ToId,
    VaultBumpRevisionState BumpRevision,
    VaultSynchronizePropertiesState SynchronizeProperties,
    VaultEnforceChildState EnforceChildState,
    VaultEnforceContentState EnforceContentState,
    VaultFileLinkTypeState ItemFileLnkUptodate,
    VaultFileLinkTypeState ItemFileLnkState,
    bool VerifyThatChildIsNotObsolete,
    bool TransitionBasedSecurity,
    bool UpdateItems);
