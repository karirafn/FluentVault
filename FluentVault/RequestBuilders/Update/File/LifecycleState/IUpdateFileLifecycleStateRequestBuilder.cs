namespace FluentVault;

public interface IUpdateFileLifecycleStateRequestBuilder
{
    public IWithFiles ByMasterId(MasterId masterId);
    public IWithFiles ByMasterIds(IEnumerable<MasterId> masterId);
    public IWithFiles ByFilename(string filename);
    public IWithFiles ByFilenames(IEnumerable<string> filenames);
}
