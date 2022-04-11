using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.Domain.Search;

internal record VaultFileSearchResult(IEnumerable<VaultFile> Files, SearchStatus SearchStatus, string Bookmark = "")
{
    internal static VaultFileSearchResult Parse(XDocument document)
        => new(VaultFile.ParseAll(document),
            document.ParseElement("searchstatus", SearchStatus.Parse),
            document.GetElementValue("bookmark"));
}
