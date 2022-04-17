namespace FluentVault.Domain.Search.Files;
internal record VaultSearchFilesResponse(VaultSearchFilesResult Result, SearchStatus SearchStatus, string Bookmark = "");
