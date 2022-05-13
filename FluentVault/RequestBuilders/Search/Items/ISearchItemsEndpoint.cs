namespace FluentVault;
public interface ISearchItemsEndpoint
{
    public ISearchItemsRequestBuilder And { get; }
    public ISearchItemsEndpoint AllVersions { get; }
    public Task<IEnumerable<VaultItem>> GetAllResultsAsync();
    public Task<IEnumerable<VaultItem>> GetPagedResultAsync(int pagingLimit = 200);
    public Task<VaultItem?> GetFirstResultAsync();
}
