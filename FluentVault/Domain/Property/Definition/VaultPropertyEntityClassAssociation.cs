using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultPropertyEntityClassAssociation(VaultEntityClass EntityClass, VaultPropertyAllowedMappingDirection AllowedMappingDirection)
{
    internal static VaultPropertyEntityClassAssociation Parse(XElement element)
        => new(element.ParseAttributeValue("EntClassId", x => VaultEntityClass.FromName(x)),
            element.ParseAttributeValue("MapDirection", x => VaultPropertyAllowedMappingDirection.FromName(x)));
}
