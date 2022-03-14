
using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;

internal record SignOutCommand() : IRequest;

internal class SignOutHandler : IRequestHandler<SignOutCommand>
{
    private const string Operation = "SignOut";

    private readonly IVaultRequestService _vaultRequestService;
    private readonly VaultSessionCredentials _session;

    public SignOutHandler(IVaultRequestService vaultRequestService, VaultSessionCredentials session)
        => (_vaultRequestService, _session) = (vaultRequestService, session);

    public async Task<Unit> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        _ = await _vaultRequestService.SendAsync(Operation, _session);

        return Unit.Value;
    }
}
