
using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;

internal record SignOutCommand(VaultSessionCredentials Session) : IRequest;

internal class SignOutHandler : IRequestHandler<SignOutCommand>
{
    private const string Operation = "SignOut";

    private readonly IVaultRequestService _vaultRequestService;

    public SignOutHandler(IVaultRequestService vaultRequestService)
        => _vaultRequestService = vaultRequestService;

    public async Task<Unit> Handle(SignOutCommand command, CancellationToken cancellationToken)
    {
        _ = await _vaultRequestService.SendAsync(Operation, command.Session);

        return Unit.Value;
    }
}
