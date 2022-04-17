using FluentVault.Domain.SecurityHeader;
using FluentVault.Features;

using MediatR;

namespace FluentVault.Common;
internal class VaultSession : IAsyncDisposable
{
    private readonly IMediator _mediator;

    public VaultSession(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Start()
        => SecurityHeader = await _mediator.Send(new SignInCommand());

    public VaultSecurityHeader? SecurityHeader { get; private set; }

    public async ValueTask DisposeAsync()
    {
        await _mediator.Send(new SignOutCommand());
        GC.SuppressFinalize(this);
    }
}
