using FluentVault.Domain;
using FluentVault.Features;

using MediatR;

namespace FluentVault.RequestBuilders;

internal class GetRequestBuilder : IGetRequestBuilder
{
    private readonly IMediator _mediator;
    private readonly VaultSessionCredentials _session;

    public GetRequestBuilder(IMediator mediator, VaultSessionCredentials session)
    {
        _mediator = mediator;
        _session = session;
    }

    public async Task<IEnumerable<VaultCategory>> Categories()
        => await _mediator.Send(new GetCategoriesQuery(_session));

    public async Task<IEnumerable<VaultLifeCycle>> Lifecycles()
        => await _mediator.Send(new GetAllLifeCycleDefinitionsQuery(_session));

    public async Task<IEnumerable<VaultPropertyDefinition>> Properties()
        => await _mediator.Send(new GetPropertyDefinitionsQuery(_session));
}
