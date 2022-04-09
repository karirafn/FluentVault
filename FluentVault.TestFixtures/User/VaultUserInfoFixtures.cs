using System.Text;

using AutoFixture;

namespace FluentVault.TestFixtures;
public static partial class VaultResponseFixtures
{
    public static (string Body, IEnumerable<VaultUserInfo> UserInfos) GetVaultUserInfoFixture(int count)
    {
        Fixture fixture = new();
        fixture.Register(() => VaultAuthenticationType.ActiveDirectory);

        return CreateBody<VaultUserInfo>(fixture, count, "GetUserInfosByUserIds", "http://AutodeskDM/Services/Admin/1/7/2020/", CreateUserInfoBody);
    }

    private static string CreateUserInfoBody(VaultUserInfo userInfo)
        => new StringBuilder()
            .Append("<UserInfo>")
            .Append(CreateUserBody(userInfo.User))
            .Append(userInfo.Roles.Aggregate(new StringBuilder(), (builder, role) => builder.Append(CreateRoleBody(role))))
            .Append(userInfo.Vaults.Aggregate(new StringBuilder(), (builder, vault) => builder.Append(CreateInstanceBody(vault))))
            .Append(userInfo.Groups.Aggregate(new StringBuilder(), (builder, group) => builder.Append(CreateGroupBody(group))))
            .Append("</UserInfo>")
            .ToString();
}
