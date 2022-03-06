using System.Xml.Linq;

using FluentVault.Common.Extensions;

namespace FluentVault;

public record PropertyConstraint(
    long Id,
    long PropertyDefinitionId,
    long CategoryId,
    PropertyConstraintType Type,
    bool Value)
{
    internal static PropertyConstraint Parse(XElement element)
        => new(element.ParseAttributeValue("Id", long.Parse),
            element.ParseAttributeValue("PropDefId", long.Parse),
            element.ParseAttributeValue("CatId", long.Parse),
            element.ParseAttributeValue("PropConstrTyp", x => PropertyConstraintType.FromName(x)),
            element.ParseAttributeValue("Val", bool.Parse));
}
