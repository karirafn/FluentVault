using System.Xml.Linq;

namespace FluentVault.TestFixtures.User;
public class VaultUserInfoFixture : VaultEntityRequestFixture<VaultUserInfo>
{
    public VaultUserInfoFixture() : base("GetUserInfosByUserIds", "http://AutodeskDM/Services/Admin/1/7/2020/")
    {
        Fixture = new SmartEnumFixture();
    }

    public override XElement ParseXElement(VaultUserInfo userInfo)
    {
        VaultInstanceFixture instanceFixture = new(Namespace);
        VaultGroupFixture groupFixture = new(Namespace);
        VaultRoleFixture roleFixture = new(Namespace);
        XElement element = new(Namespace + "UserInfo");
        element.Add(new VaultUserFixture(Namespace).ParseXElement(userInfo.User));
        userInfo.Roles.ToList().ForEach(role => element.Add(roleFixture.ParseXElement(role)));
        userInfo.Vaults.ToList().ForEach(instance => element.Add(instanceFixture.ParseXElement(instance)));
        userInfo.Groups.ToList().ForEach(group => element.Add(groupFixture.ParseXElement(group)));

        return element;
    }
}
