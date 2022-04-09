using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.User;
public class VaultRoleFixture : VaultEntityFixture<VaultRole>
{
    public VaultRoleFixture(XNamespace @namespace) : base(@namespace)
    {
    }

    public override XElement ParseXElement(VaultRole role)
    {
        XElement element = new(Namespace + "Roles");
        element.AddAttribute("Id", role.Id);
        element.AddAttribute("Name", role.Name);
        element.AddAttribute("SysName", role.SystemName);
        element.AddAttribute("IsSys", role.IsSystemRole);
        element.AddAttribute("Descr", role.Description);

        return element;
    }
}
