using FluentVault.Domain;
using FluentVault.Features;

using MediatR;

namespace FluentVault.RequestBuilders;

internal class GetRequestBuilder : IGetRequestBuilder
{
    private readonly IMediator _mediator;
    private readonly VaultSessionCredentials _session;

    public GetRequestBuilder(IMediator mediator, VaultSessionCredentials session)
        => (_mediator, _session) = (mediator, session);

    public async Task<IEnumerable<VaultCategory>> CategoryConfigurations()
        => await _mediator.Send(new GetCategoryConfigurationsQuery(_session));

    public async Task<IEnumerable<VaultLifeCycleDefinition>> LifeCycleDefinitions()
        => await _mediator.Send(new GetAllLifeCycleDefinitionsQuery(_session));

    public async Task<IEnumerable<VaultProperty>> PropertyDefinitionInfos()
        => await _mediator.Send(new GetPropertyDefinitionInfosQuery(_session));
}
