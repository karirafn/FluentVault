using System.Xml.Linq;

using FluentVault.Common.Extensions;

namespace FluentVault;

public record EntityClassAssociation(EntityClass EntityClass, AllowedMappingDirection AllowedMappingDirection)
{
    internal static EntityClassAssociation Parse(XElement element)
        => new(element.ParseAttributeValue("EntClassId", x => EntityClass.FromName(x)),
            element.ParseAttributeValue("MapDirection", x => AllowedMappingDirection.FromName(x)));
}
