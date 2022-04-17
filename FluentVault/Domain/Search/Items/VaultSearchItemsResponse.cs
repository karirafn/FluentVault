namespace FluentVault.Domain.Search.Items;
internal record VaultSearchItemsResponse(VaultSearchItemsResult Result, SearchStatus SearchStatus, string Bookmark = "");
