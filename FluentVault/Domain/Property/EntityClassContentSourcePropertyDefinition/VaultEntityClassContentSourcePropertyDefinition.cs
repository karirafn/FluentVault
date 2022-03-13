using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultEntityClassContentSourcePropertyDefinition(
    VaultEntityClass EntityClass,
    IEnumerable<VaultContentSourcePropertyDefinition> ContentSourcePropertyDefinitions,
    IEnumerable<VaultMappingType> MappingTypes,
    IEnumerable<long> Prioroties,
    IEnumerable<VaultMappingDirection> MappingDirections,
    IEnumerable<bool> CanCreateNew)
{
    internal static VaultEntityClassContentSourcePropertyDefinition Parse(XElement element)
        => new(element.ParseAttributeValue("EntClassId", x => VaultEntityClass.FromName(x)),
            element.ParseAllElements("CtntSrcPropDef", VaultContentSourcePropertyDefinition.Parse),
            element.ParseAllElementValues("MapTyp", x => VaultMappingType.FromName(x)),
            element.ParseAllElementValues("Priority", long.Parse),
            element.ParseAllElementValues("MapDirection", x => VaultMappingDirection.FromName(x)),
            element.ParseAllElementValues("CreateNew", bool.Parse));
}
