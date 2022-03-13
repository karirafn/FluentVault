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
        => new(VaultPropertyConstraintId.ParseFromAttribute(element, "Id"),
            VaultPropertyDefinitionId.ParseFromAttribute(element, "PropDefId"),
            VaultCategoryId.ParseFromAttribute(element, "CatId"),
            element.ParseAttributeValue("PropConstrTyp", x => VaultPropertyConstraintType.FromName(x)),
            element.ParseAttributeValue("Val", bool.Parse));
}
