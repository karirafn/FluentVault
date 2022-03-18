using FluentVault.Features;
using FluentVault.RequestBuilders;
using FluentVault.Requests.Search;
using FluentVault.Requests.Update;

using MediatR;

namespace FluentVault;

internal class VaultClient : IAsyncDisposable, IVaultClient
{
    private readonly IMediator _mediator;

    public VaultClient(IMediator mediator) => _mediator = mediator;

    public IGetRequestBuilder Get => new GetRequestBuilder(_mediator);
    public ISearchRequestBuilder Search => new SearchRequestBuilder(_mediator);
    public IUpdateRequestBuilder Update => new UpdateRequestBuilder(_mediator);

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await _mediator.Send(new SignOutCommand());
        GC.SuppressFinalize(this);
    }
}
