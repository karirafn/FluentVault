using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultPropertyConstraintSerializer : XElementSerializer<VaultPropertyConstraint>
{
    private const string PropertyConstraint = nameof(PropertyConstraint);
    private const string PropDefId = nameof(PropDefId);
    private const string CatId = nameof(CatId);
    private const string PropConstrTyp = nameof(PropConstrTyp);
    private const string Val = nameof(Val);

    public VaultPropertyConstraintSerializer(XNamespace @namespace) : base(PropertyConstraint, @namespace) { }

    internal override VaultPropertyConstraint Deserialize(XElement element)
        => new(element.ParseAttributeValue(nameof(VaultPropertyConstraint.Id), VaultPropertyConstraintId.Parse),
            element.ParseAttributeValue(PropDefId, VaultPropertyDefinitionId.Parse),
            element.ParseAttributeValue(CatId, VaultCategoryId.Parse),
            element.ParseAttributeValue(PropConstrTyp, x => VaultPropertyConstraintType.FromName(x)),
            element.ParseAttributeValue(Val, bool.Parse));

    internal override XElement Serialize(VaultPropertyConstraint constraint)
        => BaseElement
            .AddAttribute(nameof(VaultPropertyConstraint.Id), constraint.Id)
            .AddAttribute(PropDefId, constraint.PropertyDefinitionId)
            .AddAttribute(CatId, constraint.CategoryId)
            .AddAttribute(PropConstrTyp, constraint.Type)
            .AddAttribute(Val, constraint.Value);
}
