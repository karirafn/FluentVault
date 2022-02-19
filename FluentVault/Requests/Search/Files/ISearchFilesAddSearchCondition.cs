namespace FluentVault;

public interface ISearchFilesAddSearchCondition
{
    public ISearchFilesRequest And { get; }
    public Task<IEnumerable<VaultFile>> SearchAllAsync();
    public Task<VaultFile?> SearchSingleAsync();
}
