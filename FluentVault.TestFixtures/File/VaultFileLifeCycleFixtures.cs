
using System.Xml.Linq;

using AutoFixture;

namespace FluentVault.TestFixtures;
public static partial class VaultResponseFixtures
{
    public static (string Body, VaultFileLifeCycle Group) GetVaultFileLifeCycleFixture()
    {
        Fixture fixture = new();
        VaultFileLifeCycle lifeCycle = fixture.Create<VaultFileLifeCycle>();
        string body = CreateFileLifeCycleBody(lifeCycle);

        return (body, lifeCycle);
    }

    public static XElement CreateFileLifeCycleXElement(VaultFileLifeCycle lifeCycle)
    {
        XElement element = new("FileLfCyc");
        element.Add(new XAttribute("LfCycStateId", lifeCycle.StateId));
        element.Add(new XAttribute("LfCycDefId", lifeCycle.DefinitionId));
        element.Add(new XAttribute("LfCycStateName", lifeCycle.StateName));
        element.Add(new XAttribute("Consume", lifeCycle.IsReleased));
        element.Add(new XAttribute("Obsolete", lifeCycle.IsObsolete));

        return element;
    }

    private static string CreateFileLifeCycleBody(VaultFileLifeCycle lifeCycle)
        => $@"<FileLfCyc
LfCycStateId=""{lifeCycle.StateId}""
LfCycDefId=""{lifeCycle.DefinitionId}""
LfCycStateName=""{lifeCycle.StateName}""
Consume=""{lifeCycle.IsReleased}""
Obsolete=""{lifeCycle.IsObsolete}""/>";
}
