namespace FluentVault;

public interface IUpdateFilePropertyDefinitionsRequestBuilder
{
    public IUpdateFilePropertDefinitionsAction ByFileMasterIds(IEnumerable<MasterId> masterIds);
    public IUpdateFilePropertDefinitionsAction ByFileMasterId(MasterId masterId);
    public IUpdateFilePropertDefinitionsAction ByFileNames(IEnumerable<string> paths);
    public IUpdateFilePropertDefinitionsAction ByFileName(string path);
}
