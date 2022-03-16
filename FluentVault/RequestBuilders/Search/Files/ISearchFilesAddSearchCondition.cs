namespace FluentVault;

public interface ISearchFilesAddSearchCondition
{
    public ISearchFilesRequestBuilder And { get; }
    public ISearchFilesAddSearchCondition GetAllVersions();
    public Task<IEnumerable<VaultFile>> WithoutPaging();
    public Task<IEnumerable<VaultFile>> WithPaging(int maxResultCount = 200);
    public Task<VaultFile?> SearchSingleAsync();
}
