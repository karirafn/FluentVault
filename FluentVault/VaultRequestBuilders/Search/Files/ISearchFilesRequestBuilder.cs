namespace FluentVault;

public interface ISearchFilesRequestBuilder
{
    public Task<VaultFile> ByFilename(string filename);
}
