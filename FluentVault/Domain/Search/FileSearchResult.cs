using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.Domain.Search;

internal record FileSearchResult(IEnumerable<VaultFile> Files, string Bookmark = "")
{
    internal static FileSearchResult Parse(XDocument document)
        => new(VaultFile.ParseAll(document),
            document.GetElementValue("bookmark"));
}
