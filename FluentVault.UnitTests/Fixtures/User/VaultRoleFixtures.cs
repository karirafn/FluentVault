
using AutoFixture;

namespace FluentVault.UnitTests.Fixtures;
internal static partial class VaultResponseFixtures
{
    public static (string Body, VaultRole User) GetVaultRoleFixture()
    {
        Fixture fixture = new();
        VaultRole role = fixture.Create<VaultRole>();
        string body = CreateRoleBody(role);

        return (body, role);
    }

    private static string CreateRoleBody(VaultRole role)
        => $@"<Roles Id=""{role.Id}"" Name=""{role.Name}"" SysName=""{role.SystemName}"" IsSys=""{role.IsSystemRole}"" Descr=""{role.Description}""/>";
}
