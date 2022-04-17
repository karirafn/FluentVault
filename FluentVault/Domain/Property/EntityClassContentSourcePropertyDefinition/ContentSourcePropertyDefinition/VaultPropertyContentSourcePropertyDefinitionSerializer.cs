using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultPropertyContentSourcePropertyDefinitionSerializer : XElementSerializer<VaultPropertyContentSourcePropertyDefinition>
{
    private const string CtntSrcPropDef = nameof(CtntSrcPropDef);
    private const string CtntSrcId = nameof(CtntSrcId);
    private const string DispName = nameof(DispName);
    private const string MapDirection = nameof(MapDirection);
    private const string Typ = nameof(Typ);
    private const string CtntSrcDefTyp = nameof(CtntSrcDefTyp);

    public VaultPropertyContentSourcePropertyDefinitionSerializer(XNamespace @namespace) : base(CtntSrcPropDef, @namespace) { }

    internal override VaultPropertyContentSourcePropertyDefinition Deserialize(XElement element)
        => new(element.ParseAttributeValue(CtntSrcId, VaultPropertyContentSourceId.Parse),
            element.GetAttributeValue(DispName),
            element.GetAttributeValue(nameof(VaultPropertyContentSourcePropertyDefinition.Moniker)),
            element.ParseAttributeValue(MapDirection, x => VaultPropertyAllowedMappingDirection.FromName(x)),
            element.ParseAttributeValue(nameof(VaultPropertyContentSourcePropertyDefinition.CanCreateNew), bool.Parse),
            element.ParseAttributeValue(nameof(VaultPropertyContentSourcePropertyDefinition.Classification), x => VaultPropertyClassification.FromName(x)),
            element.ParseAttributeValue(Typ, x => VaultDataType.FromName(x)),
            element.ParseAttributeValue(CtntSrcDefTyp, x => VaultPropertyContentSourceDefinitionType.FromName(x)));

    internal override XElement Serialize(VaultPropertyContentSourcePropertyDefinition definition)
        => BaseElement
            .AddAttribute(CtntSrcId, definition.ContentSourceId)
            .AddAttribute(DispName, definition.DisplayName)
            .AddAttribute(nameof(VaultPropertyContentSourcePropertyDefinition.Moniker), definition.Moniker)
            .AddAttribute(MapDirection, definition.MappingDirection)
            .AddAttribute(nameof(VaultPropertyContentSourcePropertyDefinition.CanCreateNew), definition.CanCreateNew)
            .AddAttribute(nameof(VaultPropertyContentSourcePropertyDefinition.Classification), definition.Classification)
            .AddAttribute(Typ, definition.DataType)
            .AddAttribute(CtntSrcDefTyp, definition.ContentSourceDefinitionType);
}
