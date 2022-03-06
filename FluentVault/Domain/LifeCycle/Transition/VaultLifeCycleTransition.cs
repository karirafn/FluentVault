using System.Xml.Linq;

using FluentVault.Extensions;

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
    bool UpdateItems)
{
    internal static VaultLifeCycleTransition Parse(XElement element)
        => new(element.ParseAttributeValue("Id", long.Parse),
            element.ParseAttributeValue("FromId", long.Parse),
            element.ParseAttributeValue("ToId", long.Parse),
            element.ParseAttributeValue("Bump", x => BumpRevisionState.FromName(x)),
            element.ParseAttributeValue("SyncPropOption", x => SynchronizePropertiesState.FromName(x)),
            element.ParseAttributeValue("CldState", x => EnforceChildState.FromName(x)),
            element.ParseAttributeValue("CtntState", x => EnforceContentState.FromName(x)),
            element.ParseAttributeValue("ItemFileLnkUptodate", x => FileLinkTypeState.FromName(x)),
            element.ParseAttributeValue("ItemFileLnkState", x => FileLinkTypeState.FromName(x)),
            element.ParseAttributeValue("CldObsState", bool.Parse),
            element.ParseAttributeValue("TransBasedSec", bool.Parse),
            element.ParseAttributeValue("UpdateItems", bool.Parse));
}
