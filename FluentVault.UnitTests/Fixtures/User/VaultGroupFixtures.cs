
using AutoFixture;

namespace FluentVault.UnitTests.Fixtures;
internal static partial class VaultResponseFixtures
{
    public static (string Body, VaultGroup Group) GetVaultGroupFixture()
    {
        Fixture fixture = new();
        fixture.Register(() => VaultAuthenticationType.ActiveDirectory);

        VaultGroup group = fixture.Create<VaultGroup>();
        string body = CreateGroupBody(group);

        return (body, group);
    }

    private static string CreateGroupBody(VaultGroup group)
        => $@"<Groups Id=""{group.Id}""
Name=""{group.Name}""
EmailDL=""{group.Email}""
CreateUserId=""{group.CreateUserId}""
CreateDate=""{group.CreateDate:yyyy-MM-dd HH:mm:ss.fffffff}""
IsActive=""{group.IsActive}""
IsSys=""{group.IsSystemGroup}""
Auth=""{group.AuthenticationType}""/>";
}
