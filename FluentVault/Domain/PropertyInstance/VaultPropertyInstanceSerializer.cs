using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault.Domain.PropertyInstance;
internal class VaultPropertyInstanceSerializer : XElementSerializer<VaultPropertyInstance>
{
    private const string PropInst = nameof(PropInst);
    private const string PropDefId = nameof(PropDefId);
    private const string ValTyp = nameof(ValTyp);
    private const string Val = nameof(Val);
    private readonly XNamespace _namespace;

    public VaultPropertyInstanceSerializer(XNamespace @namespace) : base(PropInst, @namespace)
    {
        _namespace = @namespace;
    }

    internal override XElement Serialize(VaultPropertyInstance instance)
    {
        XElement element = BaseElement
            .AddAttribute(nameof(VaultPropertyInstance.EntityId), instance.EntityId)
            .AddAttribute(PropDefId, instance.PropertyId)
            .AddAttribute(ValTyp, instance.ValueType);

        XElement value = new(_namespace + Val);
        value.AddAttribute("xsi:type", GetXsiType(instance.ValueType));

        element.Add(value);

        return element;
}

    internal override VaultPropertyInstance Deserialize(XElement element) =>
        new(element.ParseAttributeValue(nameof(VaultPropertyInstance.EntityId), VaultEntityId.Parse),
            element.ParseAttributeValue(PropDefId, VaultPropertyDefinitionId.Parse),
            element.ParseAttributeValue(ValTyp, x => VaultDataType.FromName(x)),
            element.HasElement(Val) ? element.GetElementValue(Val) : string.Empty);

    private static string GetXsiType(VaultDataType dataType)
    {
        string type = dataType.Name switch
        {
            "Bool" => "boolean",
            _ => dataType.Name.ToLower()
        };

        return $"xsd:{type}";
    }
}
