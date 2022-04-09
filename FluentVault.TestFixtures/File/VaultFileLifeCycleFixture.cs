using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.File;
public class VaultFileLifeCycleFixture : VaultEntityFixture<VaultFileLifeCycle>
{
    public VaultFileLifeCycleFixture(XNamespace @namespace) : base(@namespace) { }

    public override XElement ParseXElement(VaultFileLifeCycle lifeCycle)
    {
        XElement element = new(Namespace + "FileLfCyc");
        element.AddAttribute("LfCycStateId", lifeCycle.StateId);
        element.AddAttribute("LfCycDefId", lifeCycle.DefinitionId);
        element.AddAttribute("LfCycStateName", lifeCycle.StateName);
        element.AddAttribute("Consume", lifeCycle.IsReleased);
        element.AddAttribute("Obsolete", lifeCycle.IsObsolete);

        return element;
    }
}
