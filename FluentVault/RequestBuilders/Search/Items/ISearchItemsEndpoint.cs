namespace FluentVault;
public interface ISearchItemsEndpoint
{
    public ISearchItemsRequestBuilder And { get; }
    public ISearchItemsEndpoint AllVersions { get; }
    public Task<IEnumerable<VaultFile>> GetAllResultsAsync();
    public Task<IEnumerable<VaultFile>> GetPagedResultAsync(int pagingLimit = 200);
    public Task<VaultFile?> GetFirstResultAsync();
}
