using System.Xml.Linq;

namespace FluentVault.ExtensionMethods;

internal static class VaultLifecycleParsingExtensions
{
    internal static IEnumerable<VaultLifecycle> ParseLifecycles(this XDocument document)
        => document.ParseAllElements("LfCycDef", ParseLifecycle);

    private static VaultLifecycle ParseLifecycle(XElement element)
        => new(element.ParseAttribute("Id", long.Parse),
            element.GetAttributeValue("Name"),
            element.GetAttributeValue("SysName"),
            element.GetAttributeValue("DispName"),
            element.GetAttributeValue("Descr"),
            element.GetAttributeValue("SysAclBeh"),
            element.ParseAllElements("State", ParseState),
            element.ParseAllElements("Trans", ParseTransition));

    private static VaultLifecycleState ParseState(this XElement element)
        => new(element.ParseAttribute("Id", long.Parse),
            element.GetAttributeValue("Name"),
            element.GetAttributeValue("DispName"),
            element.GetAttributeValue("Descr"),
            element.ParseAttribute("IsDflt", bool.Parse),
            element.ParseAttribute("LfCycDefId", long.Parse),
            element.ParseAttribute("StateBasedSec", bool.Parse),
            element.ParseAttribute("ReleasedState", bool.Parse),
            element.ParseAttribute("ObsoleteState", bool.Parse),
            element.ParseAttribute("DispOrder", long.Parse),
            element.ParseAttribute("RestrictPurgeOption", RestrictPurgeOption.Parse),
            element.ParseAttribute("ItemFileSecMode", ItemToFileSecurityMode.Parse),
            element.ParseAttribute("FolderFileSecMode", FolderFileSecurityMode.Parse),
            element.ParseAllElements("Comm", x => x.Value));

    private static VaultLifecycleTransition ParseTransition(this XElement element)
        => new(element.ParseAttribute("Id", long.Parse),
            element.ParseAttribute("FromId", long.Parse),
            element.ParseAttribute("ToId", long.Parse),
            element.ParseAttribute("Bump", BumpRevisionState.Parse),
            element.ParseAttribute("SyncPropOption", SynchronizePropertiesState.Parse),
            element.ParseAttribute("CldState", EnforceChildState.Parse),
            element.ParseAttribute("CtntState", EnforceContentState.Parse),
            element.ParseAttribute("ItemFileLnkUptodate", FileLinkTypeState.Parse),
            element.ParseAttribute("ItemFileLnkState", FileLinkTypeState.Parse),
            element.ParseAttribute("CldObsState", bool.Parse),
            element.ParseAttribute("TransBasedSec", bool.Parse),
            element.ParseAttribute("UpdateItems", bool.Parse));

}
