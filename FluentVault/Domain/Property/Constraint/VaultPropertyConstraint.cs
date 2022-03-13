using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultPropertyConstraint(
    VaultPropertyConstraintId Id,
    VaultPropertyDefinitionId PropertyDefinitionId,
    VaultCategoryId CategoryId,
    VaultPropertyConstraintType Type,
    bool Value)
{
    internal static VaultPropertyConstraint Parse(XElement element)
        => new(element.ParseAttributeValue("Id", VaultPropertyConstraintId.Parse),
            element.ParseAttributeValue("PropDefId", VaultPropertyDefinitionId.Parse),
            element.ParseAttributeValue("CatId", VaultCategoryId.Parse),
            element.ParseAttributeValue("PropConstrTyp", x => VaultPropertyConstraintType.FromName(x)),
            element.ParseAttributeValue("Val", bool.Parse));
}
