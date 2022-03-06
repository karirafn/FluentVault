using System.Xml.Linq;

using FluentVault.Common.Extensions;

namespace FluentVault.Domain.Search;

internal static class VaultFileSearchResultParsingExtensions
{
    internal static VaultFileSearchResult ParseFileSearchResult(this XDocument document)
        => new(VaultFile.ParseAll(document),
            document.GetElementValue("bookmark"));
}
