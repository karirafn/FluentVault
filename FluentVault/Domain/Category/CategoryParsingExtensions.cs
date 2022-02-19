using System.Xml.Linq;

using FluentVault.Common.Extensions;

namespace FluentVault.Domain.Category;

internal static class CategoryParsingExtensions
{
    internal static IEnumerable<VaultCategory> ParseCategories(this XDocument document)
        => document.ParseAllElements("Cat", ParseCategory);

    private static VaultCategory ParseCategory(XElement element)
        => new(element.ParseElementValue("Id", long.Parse),
            element.GetElementValue("Name"),
            element.GetElementValue("SysName"),
            element.ParseElementValue("Color", long.Parse),
            element.GetElementValue("Descr"),
            element.ParseAllElementValues("EntClassId", EntityClass.Parse));
}
