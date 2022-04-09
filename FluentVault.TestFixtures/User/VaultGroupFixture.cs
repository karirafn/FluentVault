using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.User;
public class VaultGroupFixture : VaultEntityFixture<VaultGroup>
{
    public VaultGroupFixture(XNamespace @namespace) : base(@namespace)
    {
        Fixture = new SmartEnumFixture();
    }

    public override XElement ParseXElement(VaultGroup group)
    {
        XElement element = new(Namespace + "Groups");
        element.AddAttribute("Id", group.Id);
        element.AddAttribute("Name", group.Name);
        element.AddAttribute("EmailDL", group.Email);
        element.AddAttribute("CreateUserId", group.CreateUserId);
        element.AddAttribute("CreateDate", group.CreateDate);
        element.AddAttribute("IsActive", group.IsActive);
        element.AddAttribute("IsSys", group.IsSystemGroup);
        element.AddAttribute("Auth", group.AuthenticationType);

        return element;
    }
}
