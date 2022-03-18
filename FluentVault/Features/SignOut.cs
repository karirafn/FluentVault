
using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;

internal record SignOutCommand() : IRequest;

internal class SignOutHandler : IRequestHandler<SignOutCommand>
{
    private const string Operation = "SignOut";

    private readonly IVaultService _vaultRequestService;

    public SignOutHandler(IVaultService vaultRequestService)
        => _vaultRequestService = vaultRequestService;

    public async Task<Unit> Handle(SignOutCommand command, CancellationToken cancellationToken)
    {
        _ = await _vaultRequestService.SendAsync(Operation, cancellationToken: cancellationToken, canSignIn: false);

        return Unit.Value;
    }
}
