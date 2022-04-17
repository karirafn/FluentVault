using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault.Domain.Search.Items;
internal class VaultSearchItemsResponseSerializer : XElementSerializer<VaultSearchItemsResponse>
{
    private const string FindItemRevisionsBySearchConditionsResponse = nameof(FindItemRevisionsBySearchConditionsResponse);

    private readonly VaultSearchItemsResultSerializer _resultSerializer;
    private readonly SearchStatusSerializer _statusSerializer;

    public VaultSearchItemsResponseSerializer(XNamespace @namespace) : base(FindItemRevisionsBySearchConditionsResponse, @namespace)
    {
        _resultSerializer = new(Namespace);
        _statusSerializer = new(Namespace);
    }

    internal override VaultSearchItemsResponse Deserialize(XElement element)
        => new(_resultSerializer.Deserialize(element),
            _statusSerializer.Deserialize(element),
            element.GetElementValue(nameof(VaultSearchItemsResponse.Bookmark).ToLower()));

    internal override XElement Serialize(VaultSearchItemsResponse response)
        => BaseElement.AddElement(_resultSerializer.Serialize(response.Result))
            .AddElement(_statusSerializer.Serialize(response.SearchStatus))
            .AddElement(Namespace, nameof(VaultSearchItemsResponse.Bookmark).ToLower(), response.Bookmark);
}
