using System.Xml.Linq;

namespace FluentVault.TestFixtures.File;
public class VaultFileLifeCycleFixture : VaultEntityFixture<VaultFileLifeCycle>
{
    public VaultFileLifeCycleFixture(XNamespace @namespace) : base(@namespace) { }

    public override XElement ParseXElement(VaultFileLifeCycle lifeCycle)
    {
        XElement element = new(Namespace + "FileLfCyc");
        element.Add(new XAttribute("LfCycStateId", lifeCycle.StateId));
        element.Add(new XAttribute("LfCycDefId", lifeCycle.DefinitionId));
        element.Add(new XAttribute("LfCycStateName", lifeCycle.StateName));
        element.Add(new XAttribute("Consume", lifeCycle.IsReleased));
        element.Add(new XAttribute("Obsolete", lifeCycle.IsObsolete));

        return element;
    }
}
