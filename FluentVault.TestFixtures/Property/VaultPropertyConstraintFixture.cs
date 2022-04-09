using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.Property;
public class VaultPropertyConstraintFixture : VaultEntityFixture<VaultPropertyConstraint>
{
    public VaultPropertyConstraintFixture(XNamespace @namespace) : base(@namespace)
    {
        Fixture = new SmartEnumFixture();
    }

    public XElement ParseXElement(IEnumerable<VaultPropertyConstraint> constraints)
        => ParseXElement("PropConstrArray", constraints);

    public override XElement ParseXElement(VaultPropertyConstraint constraint)
    {
        XElement element = new(Namespace + "PropertyConstraint");
        element.AddAttribute("Id", constraint.Id);
        element.AddAttribute("PropDefId", constraint.PropertyDefinitionId);
        element.AddAttribute("CatId", constraint.CategoryId);
        element.AddAttribute("PropConstrTyp", constraint.Type);
        element.AddAttribute("Val", constraint.Value);

        return element;
    }
}
