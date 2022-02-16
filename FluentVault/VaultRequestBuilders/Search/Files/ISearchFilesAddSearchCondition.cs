namespace FluentVault;

public interface ISearchFilesAddSearchCondition
{
    public ISearchFilesRequestBuilder And { get; }
    public Task<IEnumerable<VaultFile>> SearchAllAsync();
    public Task<VaultFile?> SearchSingleAsync();
}
