using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultPropertyConstraint(
    long Id,
    long PropertyDefinitionId,
    long CategoryId,
    VaultPropertyConstraintType Type,
    bool Value)
{
    internal static VaultPropertyConstraint Parse(XElement element)
        => new(element.ParseAttributeValue("Id", long.Parse),
            element.ParseAttributeValue("PropDefId", long.Parse),
            element.ParseAttributeValue("CatId", long.Parse),
            element.ParseAttributeValue("PropConstrTyp", x => VaultPropertyConstraintType.FromName(x)),
            element.ParseAttributeValue("Val", bool.Parse));
}
