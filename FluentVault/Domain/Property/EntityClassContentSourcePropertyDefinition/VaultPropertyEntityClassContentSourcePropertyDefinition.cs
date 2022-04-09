using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultPropertyEntityClassContentSourcePropertyDefinition(
    VaultEntityClass EntityClass,
    IEnumerable<VaultPropertyContentSourcePropertyDefinition> ContentSourcePropertyDefinitions,
    IEnumerable<VaultPropertyMappingType> MappingTypes,
    IEnumerable<long> Prioroties,
    IEnumerable<VaultPropertyMappingDirection> MappingDirections,
    IEnumerable<bool> CanCreateNew)
{
    internal static VaultPropertyEntityClassContentSourcePropertyDefinition Parse(XElement element)
        => new(element.ParseAttributeValue("EntClassId", x => VaultEntityClass.FromName(x)),
            element.ParseAllElements("CtntSrcPropDef", VaultPropertyContentSourcePropertyDefinition.Parse),
            element.ParseAllElementValues("MapTyp", x => VaultPropertyMappingType.FromName(x)),
            element.ParseAllElementValues("Priority", long.Parse),
            element.ParseAllElementValues("MapDirection", x => VaultPropertyMappingDirection.FromName(x)),
            element.ParseAllElementValues("CreateNew", bool.Parse));
}
