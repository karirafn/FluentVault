using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.LifeCycle;
public class VaultLifeCycleTransitionFixture : VaultEntityFixture<VaultLifeCycleTransition>
{
    public VaultLifeCycleTransitionFixture(XNamespace @namespace) : base(@namespace)
    {
        Fixture = new SmartEnumFixture();
    }

    public XElement ParseXElement(IEnumerable<VaultLifeCycleTransition> transitions)
    {
        XElement root = new(Namespace + "TransArray");
        foreach (var transition in transitions)
            root.Add(ParseXElement(transition));

        return root;
    }

    public override XElement ParseXElement(VaultLifeCycleTransition transition)
    {
        XElement element = new(Namespace + "Trans");
        element.AddAttribute("Id", transition.Id);
        element.AddAttribute("FromId", transition.FromId);
        element.AddAttribute("ToId", transition.ToId);
        element.AddAttribute("Bump", transition.BumpRevision);
        element.AddAttribute("SyncPropOption", transition.SynchronizeProperties);
        element.AddAttribute("CldState", transition.EnforceChildState);
        element.AddAttribute("CtntState", transition.EnforceContentState);
        element.AddAttribute("ItemFileLnkUptodate", transition.ItemFileLnkUptodate);
        element.AddAttribute("ItemFileLnkState", transition.ItemFileLnkState);
        element.AddAttribute("CldObsState", transition.VerifyThatChildIsNotObsolete);
        element.AddAttribute("TransBasedSec", transition.TransitionBasedSecurity);
        element.AddAttribute("UpdateItems", transition.UpdateItems);

        return element;
    }

}
