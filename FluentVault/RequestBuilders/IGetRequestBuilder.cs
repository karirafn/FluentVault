using FluentVault.RequestBuilders;

namespace FluentVault;

public interface IGetRequestBuilder
{
    public Task<IEnumerable<VaultCategory>> CategoryConfigurations(CancellationToken cancellationToken = default);
    public Task<IEnumerable<VaultLifeCycleDefinition>> LifeCycleDefinitions(CancellationToken cancellationToken = default);
    public Task<IEnumerable<VaultProperty>> PropertyDefinitionInfos(CancellationToken cancellationToken = default);
    public Task<IEnumerable<VaultUserInfo>> UserInfos(IEnumerable<VaultUserId> ids, CancellationToken cancellationToken = default);
    public Task<(Uri ThinClient, Uri ThickClient)> ClientUris(VaultMasterId masterId, CancellationToken cancellationToken = default);
    public Task<VaultFile> LatestFileByMasterId(VaultMasterId id, CancellationToken cancellationToken = default);
    public Task<VaultItem> LatestItemByMasterId(VaultMasterId id, CancellationToken cancellationToken = default);
}
