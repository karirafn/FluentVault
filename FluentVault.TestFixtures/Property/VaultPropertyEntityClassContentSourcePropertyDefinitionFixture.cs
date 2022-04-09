using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.Property;
public class VaultPropertyEntityClassContentSourcePropertyDefinitionFixture : VaultEntityFixture<VaultPropertyEntityClassContentSourcePropertyDefinition>
{
    public VaultPropertyEntityClassContentSourcePropertyDefinitionFixture(XNamespace @namespace) : base(@namespace)
    {
        Fixture = new SmartEnumFixture();
    }

    public XElement ParseXElement(IEnumerable<VaultPropertyEntityClassContentSourcePropertyDefinition> definitions)
        => ParseXElement("EntClassCtntSrcPropCfgArray", definitions);

    public override XElement ParseXElement(VaultPropertyEntityClassContentSourcePropertyDefinition definition)
    {
        XElement element = new(Namespace + "EntClassCtntSrcPropDefs");
        element.AddAttribute("EntClassId", definition.EntityClass);
        element.Add(new VaultPropertyContentSourcePropertyDefinitionFixture(Namespace).ParseXElement(definition.ContentSourcePropertyDefinitions));
        element.AddNestedElements(Namespace, "MapTypArray", "MapTyp", definition.MappingTypes);
        element.AddNestedElements(Namespace, "PriorityArray", "Priority", definition.Prioroties.Select(x => x.ToString()));
        element.AddNestedElements(Namespace, "MapDirectionArray", "MapDirection", definition.MappingDirections);
        element.AddNestedElements(Namespace, "CanCreateNewArray", "CreateNew", definition.CanCreateNew.Select(x => x.ToString()));

        return element;
    }
}
