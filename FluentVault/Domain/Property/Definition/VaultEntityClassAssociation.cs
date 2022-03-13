using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultEntityClassAssociation(VaultEntityClass EntityClass, VaultAllowedMappingDirection AllowedMappingDirection)
{
    internal static VaultEntityClassAssociation Parse(XElement element)
        => new(element.ParseAttributeValue("EntClassId", x => VaultEntityClass.FromName(x)),
            element.ParseAttributeValue("MapDirection", x => VaultAllowedMappingDirection.FromName(x)));
}
