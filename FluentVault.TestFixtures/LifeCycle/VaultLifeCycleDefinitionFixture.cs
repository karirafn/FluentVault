using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.LifeCycle;
public class VaultLifeCycleDefinitionFixture : VaultEntityRequestFixture<VaultLifeCycleDefinition>
{
    public VaultLifeCycleDefinitionFixture() : base("GetAllLifeCycleDefinitions", "http://AutodeskDM/Services/LifeCycle/1/7/2020/")
    {
        Fixture = new SmartEnumFixture();
    }

    public override XElement ParseXElement(VaultLifeCycleDefinition lifeCycle)
    {
        XElement element = new(Namespace + "LfCycDef");
        element.AddAttribute("Id", lifeCycle.Id);
        element.AddAttribute("Name", lifeCycle.Name);
        element.AddAttribute("SysName", lifeCycle.SystemName);
        element.AddAttribute("DispName", lifeCycle.DisplayName);
        element.AddAttribute("Descr", lifeCycle.Description);
        element.AddAttribute("SysAclBeh", lifeCycle.SecurityDefinition);
        element.Add(new VaultLifeCycleStateFixture(Namespace).ParseXElement(lifeCycle.States));
        element.Add(new VaultLifeCycleTransitionFixture(Namespace).ParseXElement(lifeCycle.Transitions));

        return element;
    }
}
