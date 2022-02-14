namespace FluentVault;

public interface IGetFileMasterIdRequestBuilder
{
    public Task<long> ByFilename(string filename);
}
