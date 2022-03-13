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
        => new(VaultLifeCycleStateId.ParseFromAttribute(element, "LfCycStateId"),
            VaultLifeCycleDefinitionId.ParseFromAttribute(element, "LfCycDefId"),
            element.GetAttributeValue("LfCycStateName"),
            element.ParseAttributeValue("Consume", bool.Parse),
            element.ParseAttributeValue("Obsolete", bool.Parse));
}
