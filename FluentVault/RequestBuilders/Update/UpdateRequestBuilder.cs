using FluentVault.Requests.Update.File.LifecycleState;
using FluentVault.Requests.Update.File.PropertyDefinitions;

using MediatR;

namespace FluentVault.Requests.Update;

internal class UpdateRequestBuilder : IUpdateRequestBuilder, IUpdateFileRequestBuilder
{
    private readonly IMediator _mediator;

    public UpdateRequestBuilder(IMediator mediator)
        => _mediator = mediator;

    public IUpdateFileRequestBuilder File => this;
    public IUpdateFileLifecycleStateRequestBuilder LifecycleState => new UpdateFileLifecycleStateRequestBuilder(_mediator);
    public IUpdateFilePropertyDefinitionsRequestBuilder PropertyDefinitions => new UpdateFilePropertyDefinitionsRequestBuilder(_mediator);
}
