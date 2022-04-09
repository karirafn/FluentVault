
using System.Xml.Linq;

using AutoFixture;

namespace FluentVault.TestFixtures;
public static partial class VaultResponseFixtures
{
    public static (string Body, VaultFileCategory Category) GetVaultFileCategoryFixture()
    {
        Fixture fixture = new();
        VaultFileCategory category = fixture.Create<VaultFileCategory>();
        string body = CreateFileCategoryBody(category);

        return (body, category);
    }

    public static XElement CreateFileCategoryXElement(VaultFileCategory category)
    {
        XElement element = new("Cat");
        element.Add(new XAttribute("CatId", category.Id));
        element.Add(new XAttribute("CatName", category.Name));

        return element;
    }

    private static string CreateFileCategoryBody(VaultFileCategory category)
        => $@"<Cat CatId=""{category.Id}"" CatName=""{category.Name}""/>";
}
