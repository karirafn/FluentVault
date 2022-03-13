using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultProperty(
    VaultPropertyDefinition Definition,
    IEnumerable<VaultPropertyConstraint> Constraints,
    IEnumerable<string> ListValues,
    IEnumerable<VaultEntityClassContentSourcePropertyDefinition> EntityClassContentSourcePropertyDefinitions)
{
    internal static IEnumerable<VaultProperty> ParseAll(XDocument document)
        => document.ParseAllElements("PropDefInfo", ParseProperty);

    internal static VaultProperty ParseProperty(XElement element)
        => new(element.ParseElement("PropDef", VaultPropertyDefinition.Parse),
            element.ParseAllElements("PropertyConstraint", VaultPropertyConstraint.Parse),
            element.ParseAllElements("ListVal", x => x.Value),
            element.ParseAllElements("EntClassCtntSrcPropDefs", VaultEntityClassContentSourcePropertyDefinition.Parse));
}
