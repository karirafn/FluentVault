namespace FluentVault;

public interface IUpdateFilePropertyDefinitionsRequestBuilder
{
    public IUpdateFilePropertDefinitionsAction ByFileMasterIds(IEnumerable<long> masterIds);
    public IUpdateFilePropertDefinitionsAction ByFileMasterId(long masterId);
    public IUpdateFilePropertDefinitionsAction ByFileNames(IEnumerable<string> paths);
    public IUpdateFilePropertDefinitionsAction ByFileName(string path);
}
