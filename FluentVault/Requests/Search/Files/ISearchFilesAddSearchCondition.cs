namespace FluentVault;

public interface ISearchFilesAddSearchCondition
{
    public ISearchFilesRequest And { get; }
    public Task<IEnumerable<VaultFile>> SearchWithoutPaging();
    public Task<IEnumerable<VaultFile>> SearchWithPaging(int maxResultCount = 200);
    public Task<VaultFile?> SearchSingleAsync();
}
