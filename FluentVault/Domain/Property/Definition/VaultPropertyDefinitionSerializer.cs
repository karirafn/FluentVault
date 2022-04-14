using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultPropertyDefinitionSerializer : XElementSerializer<VaultPropertyDefinition>
{
    private const string PropDef = nameof(PropDef);
    private const string Typ = nameof(Typ);
    private const string DispName = nameof(DispName);
    private const string SysName = nameof(SysName);
    private const string IsAct = nameof(IsAct);
    private const string IsBasicSrch = nameof(IsBasicSrch);
    private const string IsSys = nameof(IsSys);
    private const string EntClassAssocArray = nameof(EntClassAssocArray);

    private readonly VaultPropertyEntityClassAssociationSerializer _entityClassAssociationSerializer;

    public VaultPropertyDefinitionSerializer(XNamespace @namespace) : base(PropDef, @namespace)
    {
        _entityClassAssociationSerializer = new(Namespace);
    }

    internal override VaultPropertyDefinition Deserialize(XElement element)
    {
        element = GetSerializationElement(element);

        return new(element.ParseAttributeValue(nameof(VaultPropertyDefinition.Id), VaultPropertyDefinitionId.Parse),
                   element.ParseAttributeValue(Typ, x => VaultDataType.FromName(x)),
                   element.GetAttributeValue(DispName),
                   element.GetAttributeValue(SysName),
                   element.ParseAttributeValue(IsAct, bool.Parse),
                   element.ParseAttributeValue(IsBasicSrch, bool.Parse),
                   element.ParseAttributeValue(IsSys, bool.Parse),
                   element.ParseAttributeValue(nameof(VaultPropertyDefinition.UsageCount), long.Parse),
                   _entityClassAssociationSerializer.DeserializeMany(element));
    }

    internal override XElement Serialize(VaultPropertyDefinition definition)
        => BaseElement.AddAttribute(nameof(VaultPropertyDefinition.Id), definition.Id)
            .AddAttribute(Typ, definition.DataType)
            .AddAttribute(DispName, definition.DisplayName)
            .AddAttribute(SysName, definition.SystemName)
            .AddAttribute(IsAct, definition.IsActive)
            .AddAttribute(IsBasicSrch, definition.IsUsedInBasicSearch)
            .AddAttribute(IsSys, definition.IsSystemProperty)
            .AddAttribute(nameof(VaultPropertyDefinition.UsageCount), definition.UsageCount)
            .AddElement(_entityClassAssociationSerializer.SerializeMany(EntClassAssocArray, definition.EntityClassAssociations));
}
