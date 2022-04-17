using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultPropertyEntityClassAssociationSerializer : XElementSerializer<VaultPropertyEntityClassAssociation>
{
    private const string EntClassAssoc = nameof(EntClassAssoc);
    private const string EntClassId = nameof(EntClassId);
    private const string MapDirection = nameof(MapDirection);

    public VaultPropertyEntityClassAssociationSerializer(XNamespace @namespace) : base(EntClassAssoc, @namespace) { }

    internal override VaultPropertyEntityClassAssociation Deserialize(XElement element)
        => new(element.ParseAttributeValue(EntClassId, x => VaultEntityClass.FromName(x)),
            element.ParseAttributeValue(MapDirection, x => VaultPropertyAllowedMappingDirection.FromName(x)));

    internal override XElement Serialize(VaultPropertyEntityClassAssociation association)
        => BaseElement.AddAttribute(EntClassId, association.EntityClass)
            .AddAttribute(MapDirection, association.AllowedMappingDirection);
}
