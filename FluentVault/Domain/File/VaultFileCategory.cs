using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultFileCategory(CategoryId Id, string Name)
{
    internal static VaultFileCategory Parse(XElement element)
        => new(CategoryId.ParseFromAttribute(element, "CatId"),
            element.GetAttributeValue("CatName"));
}
