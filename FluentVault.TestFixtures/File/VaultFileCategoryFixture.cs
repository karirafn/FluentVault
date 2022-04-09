using System.Xml.Linq;

namespace FluentVault.TestFixtures.File;
public class VaultFileCategoryFixture : VaultEntityFixture<VaultFileCategory>
{
    public VaultFileCategoryFixture(XNamespace @namespace) : base(@namespace) { }

    public override XElement ParseXElement(VaultFileCategory category)
    {
        XElement element = new(Namespace + "Cat");
        element.Add(new XAttribute("CatId", category.Id));
        element.Add(new XAttribute("CatName", category.Name));

        return element;
    }
}
