namespace FluentVault;
public interface IGetRequestBuilder
{
    public IGetLatestRequestBuilder Latest { get; }
    public IGetPropertiesRequestBuilder Properties { get; }
    public IGetRevisionRequestBuilder Revision { get; }
    public Task<IEnumerable<VaultCategory>> CategoryConfigurations(CancellationToken cancellationToken = default);
    public Task<IEnumerable<VaultLifeCycleDefinition>> LifeCycleDefinitions(CancellationToken cancellationToken = default);
    public Task<IEnumerable<VaultProperty>> PropertyDefinitionInfos(CancellationToken cancellationToken = default);
    public Task<IEnumerable<VaultUserInfo>> UserInfos(IEnumerable<VaultUserId> ids, CancellationToken cancellationToken = default);
    public Task<Uri> ThickClientUri(VaultMasterId masterId, CancellationToken cancellationToken = default);
    public Task<Uri> ThinClientUri(VaultMasterId masterId, CancellationToken cancellationToken = default);
    public Task<IEnumerable<VaultFolder>> FoldersByFileMasterIds(IEnumerable<VaultMasterId> masterIds, CancellationToken cancellationToken = default);
    public Task<VaultFile> LatestFileByMasterId(VaultMasterId id, CancellationToken cancellationToken = default);
    public Task<VaultItem> LatestItemByMasterId(VaultMasterId id, CancellationToken cancellationToken = default);
}
