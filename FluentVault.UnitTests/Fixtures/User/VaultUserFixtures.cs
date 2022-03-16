
using AutoFixture;

namespace FluentVault.UnitTests.Fixtures;
internal static partial class VaultResponseFixtures
{
    public static (string Body, VaultUser User) GetVaultUserFixture()
    {
        Fixture fixture = new();
        fixture.Register(() => VaultAuthenticationType.ActiveDirectory);
        VaultUser user = fixture.Create<VaultUser>();
        string body = CreateUserBody(user);

        return (body, user);
    }

    private static string CreateUserBody(VaultUser user)
        => $@"<User Id=""{user.Id}""
Name=""{user.Name}""
FirstName=""{user.FirstName}""
LastName=""{user.LastName}""
Email=""{user.Email}""
CreateUserId=""{user.CreateUserId}""
CreateDate=""{user.CreateDate:yyyy-MM-dd HH:mm:ss.fffffff}""
IsActive=""{user.IsActive}""
IsSys=""{user.IsSystemUser}""
Auth=""{user.AuthenticationType.Name}""/>";
}
