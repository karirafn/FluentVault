using System.Xml.Linq;

namespace FluentVault.TestFixtures.File;
public class VaultFileRevisionFixture : VaultEntityFixture<VaultFileRevision>
{
    public VaultFileRevisionFixture(XNamespace @namespace) : base(@namespace) { }

    public override XElement ParseXElement(VaultFileRevision revision)
    {
        XElement element = new(Namespace + "FileRev");
        element.Add(new XAttribute("RevId", revision.Id));
        element.Add(new XAttribute("Label", revision.Label));
        element.Add(new XAttribute("MaxConsumeFileId", revision.MaximumConsumeFileId));
        element.Add(new XAttribute("MaxFileId", revision.MaximumFileId));
        element.Add(new XAttribute("RevDefId", revision.DefinitionId));
        element.Add(new XAttribute("MaxRevId", revision.MaximumRevisionId));
        element.Add(new XAttribute("Order", revision.Order));

        return element;
    }
}
