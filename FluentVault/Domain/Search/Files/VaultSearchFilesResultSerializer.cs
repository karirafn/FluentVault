using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault.Domain.Search.Files;
internal class VaultSearchFilesResultSerializer : XElementSerializer<VaultSearchFilesResult>
{
    private const string FindFilesBySearchConditionsResult = nameof(FindFilesBySearchConditionsResult);

    private readonly VaultFileSerializer _fileSerializer;

    public VaultSearchFilesResultSerializer(XNamespace @namespace) : base(FindFilesBySearchConditionsResult, @namespace)
    {
        _fileSerializer = new(@namespace);
    }

    internal override VaultSearchFilesResult Deserialize(XElement element)
    {
        element = GetSerializationElement(element);

        return new(_fileSerializer.DeserializeMany(element));
    }

    internal override XElement Serialize(VaultSearchFilesResult result)
        => BaseElement.AddElements(_fileSerializer.Serialize(result.Files));
}
