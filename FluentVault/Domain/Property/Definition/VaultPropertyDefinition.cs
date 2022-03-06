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
        => new(element.ParseElement("PropDef", ParsePropertyDefinition),
            element.ParseAllElements("PropertyConstraint", ParsePropertyConstraint),
            element.ParseAllElements("ListVal", x => x.Value),
            element.ParseAllElements("EntClassCtntSrcPropDefs", ParseEntityClassContentSourcePropertyDefinition));

    internal static PropertyDefinition ParsePropertyDefinition(XElement element)
        => new(element.ParseAttributeValue("Id", long.Parse),
            element.ParseAttributeValue("Typ", x => DataType.FromName(x)),
            element.GetAttributeValue("DispName"),
            element.GetAttributeValue("SysName"),
            element.ParseAttributeValue("IsAct", bool.Parse),
            element.ParseAttributeValue("IsBasicSrch", bool.Parse),
            element.ParseAttributeValue("IsSys", bool.Parse),
            element.ParseAttributeValue("UsageCount", long.Parse),
            element.ParseAllElements("EntClassAssoc", ParseEntityClassAssociation));

    internal static PropertyConstraint ParsePropertyConstraint(XElement element)
        => new(element.ParseAttributeValue("Id", long.Parse),
            element.ParseAttributeValue("PropDefId", long.Parse),
            element.ParseAttributeValue("CatId", long.Parse),
            element.ParseAttributeValue("PropConstrTyp", x => PropertyConstraintType.FromName(x)),
            element.ParseAttributeValue("Val", bool.Parse));

    internal static EntityClassAssociation ParseEntityClassAssociation(XElement element)
        => new(element.ParseAttributeValue("EntClassId", x => EntityClass.FromName(x)),
            element.ParseAttributeValue("MapDirection", x => AllowedMappingDirection.FromName(x)));

    internal static EntityClassContentSourcePropertyDefinition ParseEntityClassContentSourcePropertyDefinition(XElement element)
        => new(element.ParseAttributeValue("EntClassId", x => EntityClass.FromName(x)),
            element.ParseAllElements("CtntSrcPropDef", ParseContentSourcePropertyDefinition),
            element.ParseAllElementValues("MapTyp", x => MappingType.FromName(x)),
            element.ParseAllElementValues("Priority", long.Parse),
            element.ParseAllElementValues("MapDirection", x => MappingDirection.FromName(x)),
            element.ParseAllElementValues("CreateNew", bool.Parse));

    internal static ContentSourcePropertyDefinition ParseContentSourcePropertyDefinition(XElement element)
        => new(element.ParseAttributeValue("CtntSrcId", long.Parse),
            element.GetAttributeValue("DispName"),
            element.GetAttributeValue("Moniker"),
            element.ParseAttributeValue("MapDirection", x => AllowedMappingDirection.FromName(x)),
            element.ParseAttributeValue("CanCreateNew", bool.Parse),
            element.ParseAttributeValue("Classification", x => Classification.FromName(x)),
            element.ParseAttributeValue("Typ", x => DataType.FromName(x)),
            element.ParseAttributeValue("CtntSrcDefTyp", x => ContentSourceDefinitionType.FromName(x)));
}
