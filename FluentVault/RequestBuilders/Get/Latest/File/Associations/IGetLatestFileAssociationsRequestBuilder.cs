namespace FluentVault;

public interface IGetLatestFileAssociationsRequestBuilder
{
    public IGetLatestFileAssociationsEndpoint ByMasterId(VaultMasterId masterId);
    public IGetLatestFileAssociationsEndpoint ByMasterIds(IEnumerable<VaultMasterId> masterIds);
}
