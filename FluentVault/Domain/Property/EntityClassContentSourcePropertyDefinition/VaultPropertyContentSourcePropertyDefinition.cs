using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultPropertyContentSourcePropertyDefinition(
    VaultPropertyContentSourceId ContentSourceId,
    string DisplayName,
    string Moniker,
    VaultPropertyAllowedMappingDirection MappingDirection,
    bool CanCreateNew,
    VaultPropertyClassification Classification,
    VaultDataType DataType,
    VaultPropertyContentSourceDefinitionType ContentSourceDefinitionType)
{
    internal static VaultPropertyContentSourcePropertyDefinition Parse(XElement element)
        => new(element.ParseAttributeValue("CtntSrcId", VaultPropertyContentSourceId.Parse),
            element.GetAttributeValue("DispName"),
            element.GetAttributeValue("Moniker"),
            element.ParseAttributeValue("MapDirection", x => VaultPropertyAllowedMappingDirection.FromName(x)),
            element.ParseAttributeValue("CanCreateNew", bool.Parse),
            element.ParseAttributeValue("Classification", x => VaultPropertyClassification.FromName(x)),
            element.ParseAttributeValue("Typ", x => VaultDataType.FromName(x)),
            element.ParseAttributeValue("CtntSrcDefTyp", x => VaultPropertyContentSourceDefinitionType.FromName(x)));
}
