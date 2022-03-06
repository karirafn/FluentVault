using System.Xml.Linq;

using FluentVault.Common.Extensions;

namespace FluentVault;

public record VaultPropertyDefinition(
    PropertyDefinition Definition,
    IEnumerable<PropertyConstraint> Constraints,
    IEnumerable<string> ListValues,
    IEnumerable<EntityClassContentSourcePropertyDefinition> EntityClassContentSourcePropertyDefinitions)
{
    internal static IEnumerable<VaultPropertyDefinition> ParseAll(XDocument document)
        => document.ParseAllElements("PropDefInfo", ParseProperty);

    internal static VaultPropertyDefinition ParseProperty(XElement element)
        => new(element.ParseElement("PropDef", PropertyDefinition.Parse),
            element.ParseAllElements("PropertyConstraint", PropertyConstraint.Parse),
            element.ParseAllElements("ListVal", x => x.Value),
            element.ParseAllElements("EntClassCtntSrcPropDefs", EntityClassContentSourcePropertyDefinition.Parse));
}
