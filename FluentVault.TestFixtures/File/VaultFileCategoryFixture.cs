using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.File;
public class VaultFileCategoryFixture : VaultEntityFixture<VaultFileCategory>
{
    public VaultFileCategoryFixture(XNamespace @namespace) : base(@namespace) { }

    public override XElement ParseXElement(VaultFileCategory category)
    {
        XElement element = new(Namespace + "Cat");
        element.AddAttribute("CatId", category.Id);
        element.AddAttribute("CatName", category.Name);

        return element;
    }
}
