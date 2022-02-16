using System.Xml.Linq;

namespace FluentVault.ExtensionMethods;

internal static class VaultLifecycleParsingExtensions
{
    internal static IEnumerable<VaultLifecycle> ParseLifecycles(this XDocument document)
        => document.ParseAllElements("LfCycDef", ParseLifecycle);

    private static VaultLifecycle ParseLifecycle(XElement element)
        => new(element.ParseAttributeAsLong("Id"),
            element.GetAttributeValue("Name"),
            element.GetAttributeValue("SysName"),
            element.GetAttributeValue("DispName"),
            element.GetAttributeValue("Descr"),
            element.GetAttributeValue("SysAclBeh"),
            element.ParseAllElements("State", ParseState));

    private static VaultLifecycleState ParseState(this XElement element)
        => new(element.ParseAttributeAsLong("Id"),
            element.GetAttributeValue("Name"),
            element.GetAttributeValue("DispName"),
            element.GetAttributeValue("Descr"),
            element.ParseAttributeAsBool("IsDflt"),
            element.ParseAttributeAsLong("LfCycDefId"),
            element.ParseAttributeAsBool("StateBasedSec"),
            element.ParseAttributeAsBool("ReleasedState"),
            element.ParseAttributeAsBool("ObsoleteState"),
            element.ParseAttributeAsLong("DispOrder"),
            element.GetAttributeValue("RestrictPurgeOption"),
            element.GetAttributeValue("ItemFileSecMode"),
            element.GetAttributeValue("FolderFileSecMode"),
            element.ParseAllElements("Comm", x => x.Value));

}
