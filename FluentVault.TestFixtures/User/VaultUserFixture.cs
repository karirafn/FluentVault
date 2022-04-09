using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.User;
public class VaultUserFixture : VaultEntityFixture<VaultUser>
{
    public VaultUserFixture(XNamespace @namespace) : base(@namespace)
    {
        Fixture = new SmartEnumFixture();
    }

    public override XElement ParseXElement(VaultUser user)
    {
        XElement element = new(Namespace + "User");
        element.AddAttribute("Id", user.Id);
        element.AddAttribute("Name", user.Name);
        element.AddAttribute("FirstName", user.FirstName);
        element.AddAttribute("LastName", user.LastName);
        element.AddAttribute("Email", user.Email);
        element.AddAttribute("CreateUserId", user.CreateUserId);
        element.AddAttribute("CreateDate", user.CreateDate);
        element.AddAttribute("IsActive", user.IsActive);
        element.AddAttribute("IsSys", user.IsSystemUser);
        element.AddAttribute("Auth", user.AuthenticationType);

        return element;
    }
}
