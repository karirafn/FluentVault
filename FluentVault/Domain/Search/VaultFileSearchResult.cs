using System.Xml.Linq;

using FluentVault.Common.Extensions;

namespace FluentVault.Domain.Search;

internal record VaultFileSearchResult(IEnumerable<VaultFile> Files, string Bookmark = "")
{
    internal static VaultFileSearchResult Parse(XDocument document)
        => new(VaultFile.ParseAll(document),
            document.GetElementValue("bookmark"));
}
