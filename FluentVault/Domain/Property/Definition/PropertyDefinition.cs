using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record PropertyDefinition(
    long Id,
    DataType DataType,
    string DisplayName,
    string SystemName,
    bool IsActive,
    bool IsUsedInBasicSearch,
    bool IsSystemProperty,
    long UsageCount,
    IEnumerable<EntityClassAssociation> EntityClassAssociations)
{
    internal static PropertyDefinition Parse(XElement element)
        => new(element.ParseAttributeValue("Id", long.Parse),
            element.ParseAttributeValue("Typ", x => DataType.FromName(x)),
            element.GetAttributeValue("DispName"),
            element.GetAttributeValue("SysName"),
            element.ParseAttributeValue("IsAct", bool.Parse),
            element.ParseAttributeValue("IsBasicSrch", bool.Parse),
            element.ParseAttributeValue("IsSys", bool.Parse),
            element.ParseAttributeValue("UsageCount", long.Parse),
            element.ParseAllElements("EntClassAssoc", EntityClassAssociation.Parse));
}
