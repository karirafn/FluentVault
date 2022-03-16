using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultPropertyDefinition(
    VaultPropertyDefinitionId Id,
    VaultDataType DataType,
    string DisplayName,
    string SystemName,
    bool IsActive,
    bool IsUsedInBasicSearch,
    bool IsSystemProperty,
    long UsageCount,
    IEnumerable<VaultPropertyEntityClassAssociation> EntityClassAssociations)
{
    internal static VaultPropertyDefinition Parse(XElement element)
        => new(element.ParseAttributeValue("Id", VaultPropertyDefinitionId.Parse),
            element.ParseAttributeValue("Typ", x => VaultDataType.FromName(x)),
            element.GetAttributeValue("DispName"),
            element.GetAttributeValue("SysName"),
            element.ParseAttributeValue("IsAct", bool.Parse),
            element.ParseAttributeValue("IsBasicSrch", bool.Parse),
            element.ParseAttributeValue("IsSys", bool.Parse),
            element.ParseAttributeValue("UsageCount", long.Parse),
            element.ParseAllElements("EntClassAssoc", VaultPropertyEntityClassAssociation.Parse));
}
