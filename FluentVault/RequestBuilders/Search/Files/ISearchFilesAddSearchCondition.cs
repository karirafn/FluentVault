namespace FluentVault;

public interface ISearchFilesAddSearchCondition
{
    public ISearchFilesRequestBuilder And { get; }
    public Task<IEnumerable<VaultFile>> hWithoutPaging();
    public Task<IEnumerable<VaultFile>> WithPaging(int maxResultCount = 200);
    public Task<VaultFile?> SearchSingleAsync();
}
