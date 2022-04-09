using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.Property;
public class VaultPropertyEntityClassAssociationFixture : VaultEntityFixture<VaultPropertyEntityClassAssociation>
{
    public VaultPropertyEntityClassAssociationFixture(XNamespace @namespace) : base(@namespace)
    {
        Fixture = new SmartEnumFixture();
    }

    public XElement ParseXElement(IEnumerable<VaultPropertyEntityClassAssociation> associations)
        => ParseXElement("EntClassAssocArray", associations);

    public override XElement ParseXElement(VaultPropertyEntityClassAssociation association)
    {
        XElement element = new(Namespace + "EntClassAssoc");
        element.AddAttribute("EntClassId", association.EntityClass);
        element.AddAttribute("MapDirection", association.AllowedMappingDirection);

        return element;
    }
}
