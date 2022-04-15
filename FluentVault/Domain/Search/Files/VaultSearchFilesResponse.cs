namespace FluentVault;
internal record VaultSearchFilesResponse(VaultSearchFilesResult Result, SearchStatus SearchStatus, string Bookmark = "");
