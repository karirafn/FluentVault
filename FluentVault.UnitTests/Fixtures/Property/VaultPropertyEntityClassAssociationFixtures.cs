
using AutoFixture;

namespace FluentVault.UnitTests.Fixtures;
internal static partial class VaultResponseFixtures
{
    public static (string Body, VaultPropertyEntityClassAssociation Association) GetVaultPropertyEntityClassAssociationFixture()
    {
        Fixture fixture = new();
        fixture.Register(() => VaultEntityClass.File);
        fixture.Register(() => VaultPropertyAllowedMappingDirection.Write);

        VaultPropertyEntityClassAssociation association = fixture.Create<VaultPropertyEntityClassAssociation>();
        string body = CreateVaultPropertyEntityClassAssociationBody(association);

        return (body, association);
    }

    private static string CreateVaultPropertyEntityClassAssociationBody(VaultPropertyEntityClassAssociation association)
        => $@"<EntClassAssoc EntClassId=""{association.EntityClass}"" MapDirection=""{association.AllowedMappingDirection}""/>";
}
