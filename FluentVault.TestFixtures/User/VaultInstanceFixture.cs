using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.User;
public class VaultInstanceFixture : VaultEntityFixture<VaultInstance>
{
    public VaultInstanceFixture(XNamespace @namespace) : base(@namespace)
    {
    }

    public override XElement ParseXElement(VaultInstance instance)
    {
        XElement element = new(Namespace + "Vaults");
        element.AddAttribute("Id", instance.Id);
        element.AddAttribute("Name", instance.Name);
        element.AddAttribute("CreateDate", instance.CreateDate);
        element.AddAttribute("CreateUserId", instance.CreateUserId);

        return element;
    }
}
