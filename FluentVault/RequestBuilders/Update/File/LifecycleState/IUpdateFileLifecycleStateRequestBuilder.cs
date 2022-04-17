namespace FluentVault;
public interface IUpdateFileLifecycleStateRequestBuilder
{
    public IWithFiles ByMasterId(VaultMasterId masterId);
    public IWithFiles ByMasterIds(IEnumerable<VaultMasterId> masterId);
    public IWithFiles ByFilename(string filename);
    public IWithFiles ByFilenames(IEnumerable<string> filenames);
}
