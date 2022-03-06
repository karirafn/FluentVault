using System.Xml.Linq;

using FluentVault.Common.Extensions;

namespace FluentVault;

public record EntityClassContentSourcePropertyDefinition(
    EntityClass EntityClass,
    IEnumerable<ContentSourcePropertyDefinition> ContentSourcePropertyDefinitions,
    IEnumerable<MappingType> MappingTypes,
    IEnumerable<long> Prioroties,
    IEnumerable<MappingDirection> MappingDirections,
    IEnumerable<bool> CanCreateNew)
{
    internal static EntityClassContentSourcePropertyDefinition Parse(XElement element)
        => new(element.ParseAttributeValue("EntClassId", x => EntityClass.FromName(x)),
            element.ParseAllElements("CtntSrcPropDef", ContentSourcePropertyDefinition.Parse),
            element.ParseAllElementValues("MapTyp", x => MappingType.FromName(x)),
            element.ParseAllElementValues("Priority", long.Parse),
            element.ParseAllElementValues("MapDirection", x => MappingDirection.FromName(x)),
            element.ParseAllElementValues("CreateNew", bool.Parse));
}
