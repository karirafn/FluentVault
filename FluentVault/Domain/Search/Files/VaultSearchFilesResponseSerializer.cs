using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault.Domain.Search.Files;
internal class VaultSearchFilesResponseSerializer : XElementSerializer<VaultSearchFilesResponse>
{
    private const string FindFilesBySearchConditionsResponse = nameof(FindFilesBySearchConditionsResponse);

    private readonly VaultSearchFilesResultSerializer _resultSerializer;
    private readonly SearchStatusSerializer _statusSeraializer;

    public VaultSearchFilesResponseSerializer(XNamespace @namespace) : base(FindFilesBySearchConditionsResponse, @namespace)
    {
        _resultSerializer = new(Namespace);
        _statusSeraializer = new(Namespace);
    }

    internal override VaultSearchFilesResponse Deserialize(XElement element)
        => new(_resultSerializer.Deserialize(element),
            _statusSeraializer.Deserialize(element),
            element.GetElementValue(nameof(VaultSearchFilesResponse.Bookmark).ToLower()));

    internal override XElement Serialize(VaultSearchFilesResponse entity)
        => BaseElement.AddElement(_resultSerializer.Serialize(entity.Result))
            .AddElement(_statusSeraializer.Serialize(entity.SearchStatus))
            .AddElement(Namespace, nameof(VaultSearchFilesResponse.Bookmark).ToLower(), entity.Bookmark);
}
