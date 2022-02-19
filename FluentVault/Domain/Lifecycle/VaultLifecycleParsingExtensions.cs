using System.Xml.Linq;

using FluentVault.Common.Extensions;

namespace FluentVault.Domain.Lifecycle;

internal static class VaultLifecycleParsingExtensions
{
    internal static IEnumerable<VaultLifecycle> ParseLifecycles(this XDocument document)
        => document.ParseAllElements("LfCycDef", ParseLifecycle);

    private static VaultLifecycle ParseLifecycle(XElement element)
        => new(element.ParseAttributeValue("Id", long.Parse),
            element.GetAttributeValue("Name"),
            element.GetAttributeValue("SysName"),
            element.GetAttributeValue("DispName"),
            element.GetAttributeValue("Descr"),
            element.GetAttributeValue("SysAclBeh"),
            element.ParseAllElements("State", ParseState),
            element.ParseAllElements("Trans", ParseTransition));

    private static VaultLifecycleState ParseState(this XElement element)
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
            element.ParseAttributeValue("RestrictPurgeOption", RestrictPurgeOption.Parse),
            element.ParseAttributeValue("ItemFileSecMode", ItemToFileSecurityMode.Parse),
            element.ParseAttributeValue("FolderFileSecMode", FolderFileSecurityMode.Parse),
            element.ParseAllElements("Comm", x => x.Value));

    private static VaultLifecycleTransition ParseTransition(this XElement element)
        => new(element.ParseAttributeValue("Id", long.Parse),
            element.ParseAttributeValue("FromId", long.Parse),
            element.ParseAttributeValue("ToId", long.Parse),
            element.ParseAttributeValue("Bump", BumpRevisionState.Parse),
            element.ParseAttributeValue("SyncPropOption", SynchronizePropertiesState.Parse),
            element.ParseAttributeValue("CldState", EnforceChildState.Parse),
            element.ParseAttributeValue("CtntState", EnforceContentState.Parse),
            element.ParseAttributeValue("ItemFileLnkUptodate", FileLinkTypeState.Parse),
            element.ParseAttributeValue("ItemFileLnkState", FileLinkTypeState.Parse),
            element.ParseAttributeValue("CldObsState", bool.Parse),
            element.ParseAttributeValue("TransBasedSec", bool.Parse),
            element.ParseAttributeValue("UpdateItems", bool.Parse));

}
