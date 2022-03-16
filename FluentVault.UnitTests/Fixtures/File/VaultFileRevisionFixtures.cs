
using AutoFixture;

namespace FluentVault.UnitTests.Fixtures;
internal static partial class VaultResponseFixtures
{
    public static (string Body, VaultFileRevision Revision) GetVaultFileRevisionFixture()
    {
        Fixture fixture = new();
        VaultFileRevision revision = fixture.Create<VaultFileRevision>();
        string body = CreateFileRevisionBody(revision);

        return (body, revision);
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
