using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record ContentSourcePropertyDefinition(
    VaultContentSourceId ContentSourceId,
    string DisplayName,
    string Moniker,
    AllowedMappingDirection MappingDirection,
    bool CanCreateNew,
    Classification Classification,
    DataType DataType,
    ContentSourceDefinitionType ContentSourceDefinitionType)
{
    internal static ContentSourcePropertyDefinition Parse(XElement element)
        => new(element.ParseAttributeValue("CtntSrcId", VaultContentSourceId.Parse),
            element.GetAttributeValue("DispName"),
            element.GetAttributeValue("Moniker"),
            element.ParseAttributeValue("MapDirection", x => AllowedMappingDirection.FromName(x)),
            element.ParseAttributeValue("CanCreateNew", bool.Parse),
            element.ParseAttributeValue("Classification", x => Classification.FromName(x)),
            element.ParseAttributeValue("Typ", x => DataType.FromName(x)),
            element.ParseAttributeValue("CtntSrcDefTyp", x => ContentSourceDefinitionType.FromName(x)));
}
