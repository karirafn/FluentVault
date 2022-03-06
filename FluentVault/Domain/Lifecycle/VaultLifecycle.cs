using System.Xml.Linq;

using FluentVault.Common.Extensions;

namespace FluentVault;

public record VaultLifeCycle(
    long Id,
    string Name,
    string SystemName,
    string DisplayName,
    string Description,
    string SecurityDefinition,
    IEnumerable<VaultLifeCycleState> States,
    IEnumerable<VaultLifeCycleTransition> Transitions)
{
    internal static IEnumerable<VaultLifeCycle> ParseAll(XDocument document)
        => document.ParseAllElements("LfCycDef", ParseLifecycle);

    private static VaultLifeCycle ParseLifecycle(XElement element)
        => new(element.ParseAttributeValue("Id", long.Parse),
            element.GetAttributeValue("Name"),
            element.GetAttributeValue("SysName"),
            element.GetAttributeValue("DispName"),
            element.GetAttributeValue("Descr"),
            element.GetAttributeValue("SysAclBeh"),
            element.ParseAllElements("State", VaultLifeCycleState.Parse),
            element.ParseAllElements("Trans", VaultLifeCycleTransition.Parse));
}
