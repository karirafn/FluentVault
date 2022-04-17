
using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;
internal record SignOutCommand() : IRequest;
internal class SignOutHandler : IRequestHandler<SignOutCommand>
{
    private static readonly VaultRequest _request = new(
          operation: "SignOut",
          version: "Filestore/v26",
          service: "AuthService",
          command: "Connectivity.Application.VaultBase.SignOutCommand",
          @namespace: "Filestore/Auth/1/7/2020");
    private readonly IVaultService _vaultService;

    public SignOutHandler(IVaultService vaultService)
    {
        _vaultService = vaultService;
    }

    public async Task<Unit> Handle(SignOutCommand command, CancellationToken cancellationToken)
    {
        _ = await _vaultService.SendAuthenticatedAsync(_request, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}
