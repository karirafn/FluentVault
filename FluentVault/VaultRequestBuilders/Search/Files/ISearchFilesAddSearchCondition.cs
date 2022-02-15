namespace FluentVault;

public interface ISearchFilesAddSearchCondition
{
    public ISearchFilesRequestBuilder And { get; }
    public Task<VaultFile> SearchAsync();
}
