
using System.Xml.Linq;

using AutoFixture;

namespace FluentVault.TestFixtures;
public static partial class VaultResponseFixtures
{
    public static (string Body, VaultFileRevision Revision) GetVaultFileRevisionFixture()
    {
        Fixture fixture = new();
        VaultFileRevision revision = fixture.Create<VaultFileRevision>();
        string body = CreateFileRevisionBody(revision);

        return (body, revision);
    }

    public static XElement CreateFileRevisionXElement(VaultFileRevision revision)
    {
        XElement element = new("FileRev");
        element.Add(new XAttribute("RevId", revision.Id));
        element.Add(new XAttribute("Label", revision.Label));
        element.Add(new XAttribute("MaxConsumeFileId", revision.MaximumConsumeFileId));
        element.Add(new XAttribute("MaxFileId", revision.MaximumFileId));
        element.Add(new XAttribute("RevDefId", revision.DefinitionId));
        element.Add(new XAttribute("MaxRevId", revision.MaximumRevisionId));
        element.Add(new XAttribute("Order", revision.Order));

        return element;
    }

    private static string CreateFileRevisionBody(VaultFileRevision revision)
        => $@"<FileRev
RevId=""{revision.Id}""
Label=""{revision.Label}""
MaxConsumeFileId=""{revision.MaximumConsumeFileId}""
MaxFileId=""{revision.MaximumFileId}""
RevDefId=""{revision.DefinitionId}""
MaxRevId=""{revision.MaximumRevisionId}""
Order=""{revision.Order}""/>";
}
