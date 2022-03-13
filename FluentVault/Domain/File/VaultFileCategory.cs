using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultFileCategory(VaultCategoryId Id, string Name)
{
    internal static VaultFileCategory Parse(XElement element)
        => new(VaultCategoryId.ParseFromAttribute(element, "CatId"),
            element.GetAttributeValue("CatName"));
}
