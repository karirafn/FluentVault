using FluentVault.RequestBuilders;

namespace FluentVault;
public interface IUpdateFilePropertyDefinitionsRequestBuilder : IRequestBuilder
{
    public IUpdateFilePropertDefinitionsAction ByFileMasterIds(IEnumerable<VaultMasterId> masterIds);
    public IUpdateFilePropertDefinitionsAction ByFileMasterId(VaultMasterId masterId);
    public IUpdateFilePropertDefinitionsAction ByFileNames(IEnumerable<string> paths);
    public IUpdateFilePropertDefinitionsAction ByFileName(string path);
}
