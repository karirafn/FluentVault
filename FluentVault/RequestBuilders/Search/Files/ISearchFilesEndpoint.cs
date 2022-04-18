namespace FluentVault;

public interface ISearchFilesEndpoint
{
    public ISearchFilesRequestBuilder And { get; }
    public ISearchFilesEndpoint AllVersions { get; }
    public Task<IEnumerable<VaultFile>> GetAllResultsAsync();
    public Task<IEnumerable<VaultFile>> GetPagedResultAsync(int pagingLimit = 200);
    public Task<VaultFile?> GetFirstResultAsync();
}
