using System.Xml.Linq;

using FluentVault.Extensions;

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
    bool UpdateItems)
{
    internal static VaultLifeCycleTransition Parse(XElement element)
        => new(element.ParseAttributeValue("Id", VaultLifeCycleTransitionId.Parse),
            element.ParseAttributeValue("FromId", VaultLifeCycleStateId.Parse),
            element.ParseAttributeValue("ToId", VaultLifeCycleStateId.Parse),
            element.ParseAttributeValue("Bump", x => VaultBumpRevisionState.FromName(x)),
            element.ParseAttributeValue("SyncPropOption", x => VaultSynchronizePropertiesState.FromName(x)),
            element.ParseAttributeValue("CldState", x => VaultEnforceChildState.FromName(x)),
            element.ParseAttributeValue("CtntState", x => VaultEnforceContentState.FromName(x)),
            element.ParseAttributeValue("ItemFileLnkUptodate", x => VaultFileLinkTypeState.FromName(x)),
            element.ParseAttributeValue("ItemFileLnkState", x => VaultFileLinkTypeState.FromName(x)),
            element.ParseAttributeValue("CldObsState", bool.Parse),
            element.ParseAttributeValue("TransBasedSec", bool.Parse),
            element.ParseAttributeValue("UpdateItems", bool.Parse));
}
