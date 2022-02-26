namespace FluentVault;

public interface IUpdateFileLifecycleStateRequestBuilder
{
    public IWithFiles ByMasterId(long masterId);
    public IWithFiles ByMasterIds(IEnumerable<long> masterId);
    public IWithFiles ByFilename(string filename);
    public IWithFiles ByFilenames(IEnumerable<string> filenames);
}
