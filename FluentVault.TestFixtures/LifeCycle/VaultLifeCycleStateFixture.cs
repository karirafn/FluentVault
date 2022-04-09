using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.LifeCycle;
public class VaultLifeCycleStateFixture : VaultEntityFixture<VaultLifeCycleState>
{
    public VaultLifeCycleStateFixture(XNamespace @namespace) : base(@namespace)
    {
        Fixture = new SmartEnumFixture();
    }

    public XElement ParseXElement(IEnumerable<VaultLifeCycleState> states)
        => ParseXElement("StateArray", states);

    public override XElement ParseXElement(VaultLifeCycleState state)
    {
        XElement element = new(Namespace + "State");
        element.AddAttribute("ID", state.Id);
        element.AddAttribute("Name", state.Name);
        element.AddAttribute("DispName", state.DisplayName);
        element.AddAttribute("Descr", state.Description);
        element.AddAttribute("IsDflt", state.IsDefault);
        element.AddAttribute("LfCycDefId", state.LifecycleId);
        element.AddAttribute("StateBasedSec", state.HasStateBasedSecurity);
        element.AddAttribute("ReleasedState", state.IsReleasedState);
        element.AddAttribute("ObsoleteState", state.IsObsoleteState);
        element.AddAttribute("DispOrder", state.DisplayOrder);
        element.AddAttribute("RestrictPurgeOption", state.RestrictPurgeOption);
        element.AddAttribute("ItemFileSecMode", state.ItemFileSecurityMode);
        element.AddAttribute("FolderFileSecMode", state.FolderFileSecurityMode);
        element.AddNestedElements(Namespace, "CommArray", "Comm", state.Comments);

        return element;
    }
}
