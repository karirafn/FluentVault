using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultFileLifeCycle(
    long StateId,
    long DefinitionId,
    string StateName,
    bool IsReleased,
    bool IsObsolete)
{
    internal static VaultFileLifeCycle Parse(XElement element)
        => new(element.ParseAttributeValue("LfCycStateId", long.Parse),
            element.ParseAttributeValue("LfCycDefId", long.Parse),
            element.GetAttributeValue("LfCycStateName"),
            element.ParseAttributeValue("Consume", bool.Parse),
            element.ParseAttributeValue("Obsolete", bool.Parse));
}
