using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultFileLifeCycle(
    VaultLifeCycleStateId StateId,
    VaultLifeCycleDefinitionId DefinitionId,
    string StateName,
    bool IsReleased,
    bool IsObsolete)
{
    internal static VaultFileLifeCycle Parse(XElement element)
        => new(element.ParseAttributeValue("LfCycStateId", VaultLifeCycleStateId.Parse),
            element.ParseAttributeValue("LfCycDefId", VaultLifeCycleDefinitionId.Parse),
            element.GetAttributeValue("LfCycStateName"),
            element.ParseAttributeValue("Consume", bool.Parse),
            element.ParseAttributeValue("Obsolete", bool.Parse));
}
