using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault.Domain.Search;

internal class VaultSearchFoldersResultSerializer : XElementSerializer<VaultSearchFoldersResult>
{
    private readonly VaultFolderSerializer _folderSerializer;

    public VaultSearchFoldersResultSerializer(XNamespace @namespace) : base(nameof(VaultSearchFoldersResult), @namespace)
    {
        _folderSerializer = new(@namespace);
    }

    internal override VaultSearchFoldersResult Deserialize(XElement element)
        => new(_folderSerializer.DeserializeMany(element));

    internal override XElement Serialize(VaultSearchFoldersResult result)
        => BaseElement.AddElements(_folderSerializer.Serialize(result.Folders));
}
