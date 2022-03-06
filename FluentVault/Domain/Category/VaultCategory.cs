using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultCategory(
    long Id,
    string Name,
    string SystemName,
    long Color,
    string Description,
    IEnumerable<EntityClass> EntityClasses)
{
    internal static IEnumerable<VaultCategory> ParseAll(XDocument document)
        => document.ParseAllElements("Cat", ParseCategory);

    private static VaultCategory ParseCategory(XElement element)
        => new(element.ParseElementValue("Id", long.Parse),
            element.GetElementValue("Name"),
            element.GetElementValue("SysName"),
            element.ParseElementValue("Color", long.Parse),
            element.GetElementValue("Descr"),
            element.ParseAllElementValues("EntClassId", x => EntityClass.FromName(x)));
}
