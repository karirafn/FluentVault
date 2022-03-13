using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultPropertyDefinitionInfo(
    PropertyDefinition Definition,
    IEnumerable<PropertyConstraint> Constraints,
    IEnumerable<string> ListValues,
    IEnumerable<EntityClassContentSourcePropertyDefinition> EntityClassContentSourcePropertyDefinitions)
{
    internal static IEnumerable<VaultPropertyDefinitionInfo> ParseAll(XDocument document)
        => document.ParseAllElements("PropDefInfo", ParseProperty);

    internal static VaultPropertyDefinitionInfo ParseProperty(XElement element)
        => new(element.ParseElement("PropDef", PropertyDefinition.Parse),
            element.ParseAllElements("PropertyConstraint", PropertyConstraint.Parse),
            element.ParseAllElements("ListVal", x => x.Value),
            element.ParseAllElements("EntClassCtntSrcPropDefs", EntityClassContentSourcePropertyDefinition.Parse));
}
