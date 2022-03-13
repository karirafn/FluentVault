namespace FluentVault;

public interface IUpdateFilePropertyDefinitionsRequestBuilder
{
    public IUpdateFilePropertDefinitionsAction ByFileMasterIds(IEnumerable<VaultMasterId> masterIds);
    public IUpdateFilePropertDefinitionsAction ByFileMasterId(VaultMasterId masterId);
    public IUpdateFilePropertDefinitionsAction ByFileNames(IEnumerable<string> paths);
    public IUpdateFilePropertDefinitionsAction ByFileName(string path);
}
