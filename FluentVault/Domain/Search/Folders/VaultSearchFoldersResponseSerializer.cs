using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault.Domain.Search;

internal class VaultSearchFoldersResponseSerializer : XElementSerializer<VaultSearchFoldersResponse>
{
    private readonly VaultSearchFoldersResultSerializer _resultSerializer;
    private readonly SearchStatusSerializer _statusSeraializer;

    public VaultSearchFoldersResponseSerializer(XNamespace @namespace) : base(nameof(VaultSearchFoldersResponse), @namespace)
    {
        _resultSerializer = new(Namespace);
        _statusSeraializer = new(Namespace);
    }

    internal override VaultSearchFoldersResponse Deserialize(XElement element)
        => new(_resultSerializer.Deserialize(element),
            _statusSeraializer.Deserialize(element),
            element.GetElementValue(nameof(VaultSearchFoldersResponse.Bookmark).ToLower()));

    internal override XElement Serialize(VaultSearchFoldersResponse entity)
        => BaseElement.AddElement(_resultSerializer.Serialize(entity.Result))
            .AddElement(_statusSeraializer.Serialize(entity.SearchStatus))
            .AddElement(Namespace, nameof(VaultSearchFoldersResponse.Bookmark).ToLower(), entity.Bookmark);
}
