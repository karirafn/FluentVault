using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.Property;
public class VaultPropertyDefinitionFixture : VaultEntityFixture<VaultPropertyDefinition>
{
    public VaultPropertyDefinitionFixture(XNamespace @namespace) : base(@namespace)
    {
    }

    public override XElement ParseXElement(VaultPropertyDefinition definition)
    {
        XElement element = new(Namespace + "PropDef");
        element.AddAttribute("Id", definition.Id);
        element.AddAttribute("Typ", definition.DataType);
        element.AddAttribute("DispName", definition.DisplayName);
        element.AddAttribute("SysName", definition.SystemName);
        element.AddAttribute("IsAct", definition.IsActive);
        element.AddAttribute("IsBasicSrch", definition.IsUsedInBasicSearch);
        element.AddAttribute("IsSys", definition.IsSystemProperty);
        element.AddAttribute("UsageCount", definition.UsageCount);
        element.Add(new VaultPropertyEntityClassAssociationFixture(Namespace).ParseXElement(definition.EntityClassAssociations));

        return element;
    }
}
