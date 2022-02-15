namespace FluentVault;

public interface IAddSearchFilesCondition
{
    public ISearchFilesRequestBuilder And { get; }
    public Task<VaultFile> SearchAsync();
}
