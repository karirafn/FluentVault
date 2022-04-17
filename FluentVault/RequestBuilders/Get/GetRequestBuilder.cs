using FluentVault.Features;

using MediatR;

namespace FluentVault.RequestBuilders.Get;
internal class GetRequestBuilder : IRequestBuilder, IGetRequestBuilder
{
    private readonly IMediator _mediator;

    public GetRequestBuilder(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IEnumerable<VaultCategory>> CategoryConfigurations(CancellationToken cancellationToken = default)
        => await _mediator.Send(new GetAllCategoryConfigurationsQuery(), cancellationToken);

    public async Task<IEnumerable<VaultLifeCycleDefinition>> LifeCycleDefinitions(CancellationToken cancellationToken = default)
        => await _mediator.Send(new GetAllLifeCycleDefinitionsQuery(), cancellationToken);

    public async Task<IEnumerable<VaultProperty>> PropertyDefinitionInfos(CancellationToken cancellationToken = default)
        => await _mediator.Send(new GetAllPropertyDefinitionInfosQuery(), cancellationToken);

    public async Task<IEnumerable<VaultUserInfo>> UserInfos(IEnumerable<VaultUserId> ids, CancellationToken cancellationToken = default)
        => await _mediator.Send(new GetUserInfosByIserIdsQuery(ids), cancellationToken);

    public async Task<(Uri ThinClient, Uri ThickClient)> ClientUris(VaultMasterId masterId, CancellationToken cancellationToken = default)
        => await _mediator.Send(new GetClientUrisQuery(masterId), cancellationToken);

    public async Task<VaultFile> LatestFileByMasterId(VaultMasterId id, CancellationToken cancellationToken = default)
        => await _mediator.Send(new GetLatestFileByMasterIdQuery(id), cancellationToken);

    public async Task<VaultItem> LatestItemByMasterId(VaultMasterId id, CancellationToken cancellationToken = default)
        => await _mediator.Send(new GetLatestItemByItemMasterIdQuery(id), cancellationToken);
}
