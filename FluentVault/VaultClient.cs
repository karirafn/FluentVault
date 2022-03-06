using FluentVault.Features;
using FluentVault.RequestBuilders;
using FluentVault.Requests.Search;
using FluentVault.Requests.Update;

using MediatR;

using Microsoft.Extensions.Options;

namespace FluentVault;

internal class VaultClient : IAsyncDisposable, IVaultClient
{
    private readonly VaultSessionCredentials _session;
    private readonly VaultOptions _options;
    private readonly IMediator _mediator;

    public VaultClient(IMediator mediator, IOptions<VaultOptions> options)
    {
        (_mediator, _options) = (mediator, options.Value);

        _session = _options.AutoLogin
            ? SignIn().GetAwaiter().GetResult()
            : new();
    }

    public async Task<VaultSessionCredentials> SignIn()
        => await _mediator.Send(new SignInCommand(_options));

    public IGetRequestBuilder Get => new GetRequestBuilder(_mediator, _session);
    public ISearchRequestBuilder Search => new SearchRequestBuilder(_mediator, _session);
    public IUpdateRequestBuilder Update => new UpdateRequestBuilder(_mediator, _session);

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await _mediator.Send(new SignOutCommand());
        GC.SuppressFinalize(this);
    }
}
