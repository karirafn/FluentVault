using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultEntityCategory(VaultCategoryId Id, string Name)
{
    internal static VaultEntityCategory Parse(XElement element)
        => new(element.ParseAttributeValue("CatId", VaultCategoryId.Parse),
            element.GetAttributeValue("CatName"));
}
