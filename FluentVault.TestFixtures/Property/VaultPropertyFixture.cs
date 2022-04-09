using System.Xml.Linq;

namespace FluentVault.TestFixtures.Property;
public class VaultPropertyFixture : VaultEntityRequestFixture<VaultProperty>
{
    public VaultPropertyFixture() : base("GetPropertyDefinitionInfosByEntityClassId", "http://AutodeskDM/Services/Property/1/7/2020/")
    {
        Fixture = new SmartEnumFixture();
    }

    public override XElement ParseXElement(VaultProperty entity)
    {
        XElement element = new(Namespace + "PropDefInfo");
        element.Add(new VaultPropertyDefinitionFixture(Namespace).ParseXElement(entity.Definition));
        element.Add(new VaultPropertyListValueFixture(Namespace).ParseXElement(entity.ListValues));
        element.Add(new VaultPropertyConstraintFixture(Namespace).ParseXElement(entity.Constraints));
        element.Add(new VaultPropertyEntityClassContentSourcePropertyDefinitionFixture(Namespace).ParseXElement(entity.EntityClassContentSourcePropertyDefinitions));

        return element;
    }
}
