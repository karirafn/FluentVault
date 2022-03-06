using System.Xml.Linq;

using FluentVault.Common.Extensions;

namespace FluentVault.Domain.Lifecycle;

internal static class VaultLifecycleParsingExtensions
{
    internal static IEnumerable<VaultLifeCycle> ParseLifeCycles(this XDocument document)
        => document.ParseAllElements("LfCycDef", ParseLifecycle);

    private static VaultLifeCycle ParseLifecycle(XElement element)
        => new(element.ParseAttributeValue("Id", long.Parse),
            element.GetAttributeValue("Name"),
            element.GetAttributeValue("SysName"),
            element.GetAttributeValue("DispName"),
            element.GetAttributeValue("Descr"),
            element.GetAttributeValue("SysAclBeh"),
            element.ParseAllElements("State", ParseState),
            element.ParseAllElements("Trans", ParseTransition));

    private static VaultLifeCycleState ParseState(this XElement element)
        => new(element.ParseAttributeValue("ID", long.Parse),
            element.GetAttributeValue("Name"),
            element.GetAttributeValue("DispName"),
            element.GetAttributeValue("Descr"),
            element.ParseAttributeValue("IsDflt", bool.Parse),
            element.ParseAttributeValue("LfCycDefId", long.Parse),
            element.ParseAttributeValue("StateBasedSec", bool.Parse),
            element.ParseAttributeValue("ReleasedState", bool.Parse),
            element.ParseAttributeValue("ObsoleteState", bool.Parse),
            element.ParseAttributeValue("DispOrder", long.Parse),
            element.ParseAttributeValue("RestrictPurgeOption", x => RestrictPurgeOption.FromName(x)),
            element.ParseAttributeValue("ItemFileSecMode", x => ItemToFileSecurityMode.FromName(x)),
            element.ParseAttributeValue("FolderFileSecMode", x => FolderFileSecurityMode.FromName(x)),
            element.ParseAllElements("Comm", x => x.Value));

    private static VaultLifeCycleTransition ParseTransition(this XElement element)
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
