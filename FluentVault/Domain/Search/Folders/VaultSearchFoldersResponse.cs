namespace FluentVault;
internal record VaultSearchFoldersResponse(VaultSearchFoldersResult Result, SearchStatus SearchStatus, string Bookmark = "");
