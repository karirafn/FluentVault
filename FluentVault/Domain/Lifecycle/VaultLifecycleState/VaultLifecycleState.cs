using System.Xml.Linq;

using FluentVault.Common.Extensions;

namespace FluentVault;

public record VaultLifeCycleState(
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
    RestrictPurgeOption RestrictPurgeOption,
    ItemToFileSecurityMode ItemFileSecurityMode,
    FolderFileSecurityMode FolderFileSecurityMode,
    IEnumerable<string> Comments)
{
    internal static VaultLifeCycleState Parse(XElement element)
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
}
