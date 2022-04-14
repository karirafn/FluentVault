using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.Property;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultPropertySerializer : XElementSerializer<VaultProperty>
{
    private const string PropDefInfo = nameof(PropDefInfo);
    private const string PropConstrArray = nameof(PropConstrArray);
    private const string ListValArray = nameof(ListValArray);
    private const string EntClassCtntSrcPropCfgArray = nameof(EntClassCtntSrcPropCfgArray);

    private readonly VaultPropertyDefinitionSerializer _definitionSerializer;
    private readonly VaultPropertyConstraintSerializer _constraintSerializer;
    private readonly VaultPropertyListValueSerializer _listValueSerializer;
    private readonly VaultPropertyEntityClassContentSourcePropertyDefinitionSerializer _contentSourceSerializer;

    public VaultPropertySerializer(XNamespace @namespace) : base(PropDefInfo, @namespace) 
    {
        _definitionSerializer = new(Namespace);
        _constraintSerializer = new(Namespace);
        _listValueSerializer = new(Namespace);
        _contentSourceSerializer = new(Namespace);
    }

    internal override VaultProperty Deserialize(XElement element)
        => new(
            _definitionSerializer.Deserialize(element),
            _constraintSerializer.DeserializeMany(element),
            _listValueSerializer.DeserializeMany(element),
            _contentSourceSerializer.DeserializeMany(element));

    internal override XElement Serialize(VaultProperty property)
        => BaseElement
            .AddElement(_definitionSerializer.Serialize(property.Definition))
            .AddElement(_constraintSerializer.SerializeMany(PropConstrArray, property.Constraints))
            .AddElement(_listValueSerializer.SerializeMany(ListValArray, property.ListValues))
            .AddElement(_contentSourceSerializer.SerializeMany(EntClassCtntSrcPropCfgArray, property.EntityClassContentSourcePropertyDefinitions));
}
