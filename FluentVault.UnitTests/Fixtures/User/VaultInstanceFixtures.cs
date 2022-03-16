
using AutoFixture;

namespace FluentVault.UnitTests.Fixtures;
internal static partial class VaultResponseFixtures
{
    public static (string Body, VaultInstance Instance) GetVaultInstanceFixture()
    {
        Fixture fixture = new();
        VaultInstance instance = fixture.Create<VaultInstance>();
        string body = CreateInstanceBody(instance);

        return (body, instance);
    }

    private static string CreateInstanceBody(VaultInstance instance)
        => $@"<Vaults Id=""{instance.Id}"" Name=""{instance.Name}"" CreateDate=""{instance.CreateDate:yyyy-MM-dd HH:mm:ss.fffffff}"" CreateUserId=""{instance.CreateUserId}""/>";
}
