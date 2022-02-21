namespace FluentVault;

public interface IUpdateFilePropertyDefinitionsRequest
{
    public IUpdateFilePropertDefinitionsAction ByFileMasterIds(IEnumerable<long> masterIds);
    public IUpdateFilePropertDefinitionsAction ByFileMasterId(long masterId);
    public IUpdateFilePropertDefinitionsAction ByFileNames(IEnumerable<string> paths);
    public IUpdateFilePropertDefinitionsAction ByFileName(string path);
}
