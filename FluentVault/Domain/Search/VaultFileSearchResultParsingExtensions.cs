using System.Xml.Linq;

using FluentVault.Common.Extensions;
using FluentVault.Domain.Files;

namespace FluentVault.Domain.Search;

internal static class VaultFileSearchResultParsingExtensions
{
    internal static VaultFileSearchResult ParseFileSearchResult(this XDocument document)
        => new(document.ParseAllVaultFiles(),
            document.GetElementValue("bookmark"));
}
