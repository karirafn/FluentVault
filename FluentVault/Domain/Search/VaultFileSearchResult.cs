namespace FluentVault.Domain.Search;

internal record VaultFileSearchResult(IEnumerable<VaultFile> Files, string Bookmark = "");
