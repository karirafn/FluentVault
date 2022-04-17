using FluentVault.RequestBuilders;

namespace FluentVault;
public interface IUpdateFileLifecycleStateRequestBuilder : IRequestBuilder
{
    public IWithFiles ByMasterId(VaultMasterId masterId);
    public IWithFiles ByMasterIds(IEnumerable<VaultMasterId> masterId);
    public IWithFiles ByFilename(string filename);
    public IWithFiles ByFilenames(IEnumerable<string> filenames);
}
