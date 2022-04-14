namespace FluentVault;
public record VaultSearchFilesResponse(VaultSearchFilesResult Result, SearchStatus SearchStatus, string Bookmark = "");
