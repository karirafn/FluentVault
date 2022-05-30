namespace FluentVault;

public interface IGetFilesRequestBuilder
{
    public Task<IEnumerable<VaultFile>> ByMasterIds(IEnumerable<VaultMasterId> masterIds);
}
