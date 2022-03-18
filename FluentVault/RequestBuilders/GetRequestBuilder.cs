using FluentVault.Features;

using MediatR;

namespace FluentVault.RequestBuilders;

internal class GetRequestBuilder : IGetRequestBuilder
{
    private readonly IMediator _mediator;

    public GetRequestBuilder(IMediator mediator)
        => _mediator = mediator;

    public async Task<IEnumerable<VaultCategory>> CategoryConfigurations()
        => await _mediator.Send(new GetCategoryConfigurationsQuery());

    public async Task<IEnumerable<VaultLifeCycleDefinition>> LifeCycleDefinitions()
        => await _mediator.Send(new GetAllLifeCycleDefinitionsQuery());

    public async Task<IEnumerable<VaultProperty>> PropertyDefinitionInfos()
        => await _mediator.Send(new GetPropertyDefinitionInfosQuery());

    public async Task<IEnumerable<VaultUserInfo>> UserInfos(IEnumerable<VaultUserId> ids)
        => await _mediator.Send(new GetUserInfosByIserIdsQuery(ids));
}
