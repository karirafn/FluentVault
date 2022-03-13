using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultFileCategory(VaultCategoryId Id, string Name)
{
    internal static VaultFileCategory Parse(XElement element)
        => new(element.ParseAttributeValue("CatId", VaultCategoryId.Parse),
            element.GetAttributeValue("CatName"));
}
