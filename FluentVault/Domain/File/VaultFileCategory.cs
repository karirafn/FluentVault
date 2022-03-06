using System.Xml.Linq;

using FluentVault.Common.Extensions;

namespace FluentVault;

public record VaultFileCategory(long Id, string Name)
{
    internal static VaultFileCategory Parse(XElement element)
        => new(element.ParseAttributeValue("CatId", long.Parse),
            element.GetAttributeValue("CatName"));
}
