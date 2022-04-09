using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.Property;
public class VaultPropertyContentSourcePropertyDefinitionFixture : VaultEntityFixture<VaultPropertyContentSourcePropertyDefinition>
{
    public VaultPropertyContentSourcePropertyDefinitionFixture(XNamespace @namespace) : base(@namespace)
    {
        Fixture = new SmartEnumFixture();
    }

    public XElement ParseXElement(IEnumerable<VaultPropertyContentSourcePropertyDefinition> definitions)
        => ParseXElement("CtntSrcPropDefArray", definitions);

    public override XElement ParseXElement(VaultPropertyContentSourcePropertyDefinition definition)
    {
        XElement element = new(Namespace + "CtntSrcPropDef");
        element.AddAttribute("CtntSrcId", definition.ContentSourceId);
        element.AddAttribute("DispName", definition.DisplayName);
        element.AddAttribute("CtDispNamentSrcId", definition.DisplayName);
        element.AddAttribute("Moniker", definition.Moniker);
        element.AddAttribute("MapDirection", definition.MappingDirection);
        element.AddAttribute("CanCreateNew", definition.CanCreateNew);
        element.AddAttribute("Classification", definition.Classification);
        element.AddAttribute("Typ", definition.DataType);
        element.AddAttribute("CtntSrcDefTyp", definition.ContentSourceDefinitionType);

        return element;
    }
}
