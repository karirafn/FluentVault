using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultLifeCycleState(
    VaultLifeCycleStateId Id,
    string Name,
    string DisplayName,
    string Description,
    bool IsDefault,
    VaultLifeCycleDefinitionId LifecycleId,
    bool HasStateBasedSecurity,
    bool IsReleasedState,
    bool IsObsoleteState,
    long DisplayOrder,
    VaultRestrictPurgeOption RestrictPurgeOption,
    VaultItemToFileSecurityMode ItemFileSecurityMode,
    VaultFolderFileSecurityMode FolderFileSecurityMode,
    IEnumerable<string> Comments)
{
    internal static VaultLifeCycleState Parse(XElement element)
        => new(element.ParseAttributeValue("ID", VaultLifeCycleStateId.Parse),
            element.GetAttributeValue("Name"),
            element.GetAttributeValue("DispName"),
            element.GetAttributeValue("Descr"),
            element.ParseAttributeValue("IsDflt", bool.Parse),
            element.ParseAttributeValue("LfCycDefId", VaultLifeCycleDefinitionId.Parse),
            element.ParseAttributeValue("StateBasedSec", bool.Parse),
            element.ParseAttributeValue("ReleasedState", bool.Parse),
            element.ParseAttributeValue("ObsoleteState", bool.Parse),
            element.ParseAttributeValue("DispOrder", long.Parse),
            element.ParseAttributeValue("RestrictPurgeOption", x => VaultRestrictPurgeOption.FromName(x)),
            element.ParseAttributeValue("ItemFileSecMode", x => VaultItemToFileSecurityMode.FromName(x)),
            element.ParseAttributeValue("FolderFileSecMode", x => VaultFolderFileSecurityMode.FromName(x)),
            element.ParseAllElements("Comm", x => x.Value));
}
