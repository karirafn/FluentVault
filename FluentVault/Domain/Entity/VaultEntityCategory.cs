using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultEntityCategory(VaultCategoryId Id, string Name)
{
    internal static VaultEntityCategory Deserialize(XElement element)
        => new(element.ParseAttributeValue("CatId", VaultCategoryId.Parse),
            element.GetAttributeValue("CatName"));

    internal static XElement Serialize(VaultEntityCategory category, XNamespace @namespace)
    {
        XElement element = new(@namespace + "Cat");
        element.AddAttribute("CatId", category.Id);
        element.AddAttribute("CatName", category.Name);

        return element;
    }
}
