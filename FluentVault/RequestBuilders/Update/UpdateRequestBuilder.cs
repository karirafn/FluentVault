using FluentVault.Domain;
using FluentVault.Requests.Update.File.LifecycleState;
using FluentVault.Requests.Update.File.PropertyDefinitions;

using MediatR;

namespace FluentVault.Requests.Update;

internal class UpdateRequestBuilder : IUpdateRequestBuilder, IUpdateFileRequestBuilder
{
    private readonly IMediator _mediator;
    private readonly VaultSessionCredentials _session;

    public UpdateRequestBuilder(IMediator mediator, VaultSessionCredentials session)
        => (_mediator, _session) = (mediator, session);

    public IUpdateFileRequestBuilder File => this;
    public IUpdateFileLifecycleStateRequestBuilder LifecycleState => new UpdateFileLifecycleStateRequestBuilder(_mediator, _session);
    public IUpdateFilePropertyDefinitionsRequestBuilder PropertyDefinitions => new UpdateFilePropertyDefinitionsRequestBuilder(_mediator, _session);
}
