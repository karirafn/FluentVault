namespace FluentVault.Domain.Search.Folders;
internal record VaultSearchFoldersResponse(VaultSearchFoldersResult Result, SearchStatus SearchStatus, string Bookmark = "");
