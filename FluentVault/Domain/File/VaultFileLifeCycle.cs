using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultFileLifeCycle(
    LifeCycleStateId StateId,
    long DefinitionId,
    string StateName,
    bool IsReleased,
    bool IsObsolete)
{
    internal static VaultFileLifeCycle Parse(XElement element)
        => new(LifeCycleStateId.ParseFromAttribute(element, "LfCycStateId"),
            element.ParseAttributeValue("LfCycDefId", long.Parse),
            element.GetAttributeValue("LfCycStateName"),
            element.ParseAttributeValue("Consume", bool.Parse),
            element.ParseAttributeValue("Obsolete", bool.Parse));
}
