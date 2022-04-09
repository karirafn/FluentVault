using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.File;
public class VaultFileRevisionFixture : VaultEntityFixture<VaultFileRevision>
{
    public VaultFileRevisionFixture(XNamespace @namespace) : base(@namespace) { }

    public override XElement ParseXElement(VaultFileRevision revision)
    {
        XElement element = new(Namespace + "FileRev");
        element.AddAttribute("RevId", revision.Id);
        element.AddAttribute("Label", revision.Label);
        element.AddAttribute("MaxConsumeFileId", revision.MaximumConsumeFileId);
        element.AddAttribute("MaxFileId", revision.MaximumFileId);
        element.AddAttribute("RevDefId", revision.DefinitionId);
        element.AddAttribute("MaxRevId", revision.MaximumRevisionId);
        element.AddAttribute("Order", revision.Order);

        return element;
    }
}
