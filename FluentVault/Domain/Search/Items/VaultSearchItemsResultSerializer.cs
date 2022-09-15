using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.Item;
using FluentVault.Extensions;

namespace FluentVault.Domain.Search.Items;
internal class VaultSearchItemsResultSerializer : XElementSerializer<VaultSearchItemsResult>
{
    private const string FindItemRevisionsBySearchConditionsResult = nameof(FindItemRevisionsBySearchConditionsResult);

    private readonly VaultItemSerializer _itemSerializer;

    public VaultSearchItemsResultSerializer(XNamespace @namespace) : base(FindItemRevisionsBySearchConditionsResult, @namespace)
    {
        _itemSerializer = new(Namespace);
    }

    internal override VaultSearchItemsResult Deserialize(XElement element)
    {
        if (!element.HasElement(FindItemRevisionsBySearchConditionsResult))
        {
            return new(Enumerable.Empty<VaultItem>());
        }

        element = GetSerializationElement(element);

        return new(_itemSerializer.DeserializeMany(element));
    }

    internal override XElement Serialize(VaultSearchItemsResult result)
        => BaseElement.AddElements(_itemSerializer.Serialize(result.Items));
}
