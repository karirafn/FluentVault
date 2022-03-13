using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultContentSourcePropertyDefinition(
    VaultContentSourceId ContentSourceId,
    string DisplayName,
    string Moniker,
    VaultAllowedMappingDirection MappingDirection,
    bool CanCreateNew,
    VaultClassification Classification,
    VaultDataType DataType,
    VaultContentSourceDefinitionType ContentSourceDefinitionType)
{
    internal static VaultContentSourcePropertyDefinition Parse(XElement element)
        => new(element.ParseAttributeValue("CtntSrcId", VaultContentSourceId.Parse),
            element.GetAttributeValue("DispName"),
            element.GetAttributeValue("Moniker"),
            element.ParseAttributeValue("MapDirection", x => VaultAllowedMappingDirection.FromName(x)),
            element.ParseAttributeValue("CanCreateNew", bool.Parse),
            element.ParseAttributeValue("Classification", x => VaultClassification.FromName(x)),
            element.ParseAttributeValue("Typ", x => VaultDataType.FromName(x)),
            element.ParseAttributeValue("CtntSrcDefTyp", x => VaultContentSourceDefinitionType.FromName(x)));
}
