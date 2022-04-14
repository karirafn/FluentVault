using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultPropertyEntityClassContentSourcePropertyDefinitionSerializer : XElementSerializer<VaultPropertyEntityClassContentSourcePropertyDefinition>
{
    private const string EntClassCtntSrcPropDefs = nameof(EntClassCtntSrcPropDefs);
    private const string CtntSrcPropDefArray = nameof(CtntSrcPropDefArray);

    private readonly VaultPropertyContentSourcePropertyDefinitionSerializer _contentSourcePropertyDefinitionSerializer;

    public VaultPropertyEntityClassContentSourcePropertyDefinitionSerializer(XNamespace @namespace) : base(EntClassCtntSrcPropDefs, @namespace)
    {
        _contentSourcePropertyDefinitionSerializer = new(Namespace);
    }

    internal override VaultPropertyEntityClassContentSourcePropertyDefinition Deserialize(XElement element)
        => new(element.ParseAttributeValue("EntClassId", x => VaultEntityClass.FromName(x)),
            _contentSourcePropertyDefinitionSerializer.DeserializeMany(element),
            element.ParseAllElementValues("MapTyp", x => VaultPropertyMappingType.FromName(x)),
            element.ParseAllElementValues("Priority", long.Parse),
            element.ParseAllElementValues("MapDirection", x => VaultPropertyMappingDirection.FromName(x)),
            element.ParseAllElementValues("CreateNew", bool.Parse));

    internal override XElement Serialize(VaultPropertyEntityClassContentSourcePropertyDefinition definition)
        => BaseElement
            .AddAttribute("EntClassId", definition.EntityClass)
            .AddElement(_contentSourcePropertyDefinitionSerializer.SerializeMany(CtntSrcPropDefArray, definition.ContentSourcePropertyDefinitions)
            .AddNestedElements(Namespace, "MapTypArray", "MapTyp", definition.MappingTypes)
            .AddNestedElements(Namespace, "PriorityArray", "Priority", definition.Prioroties.Select(x => x.ToString()))
            .AddNestedElements(Namespace, "MapDirectionArray", "MapDirection", definition.MappingDirections)
            .AddNestedElements(Namespace, "CanCreateNewArray", "CreateNew", definition.CanCreateNew.Select(x => x.ToString())));
}
