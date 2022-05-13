
using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.SecurityHeader;

using MediatR;

namespace FluentVault.Features;
internal record SignOutCommand(VaultSecurityHeader SecurityHeader) : IRequest;
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
        XDocument requestBody = VaultRequestSerializer.Serialize(_request, command.SecurityHeader);
        _ = await _vaultService.SendAsync(_request.Uri, _request.SoapAction, requestBody, cancellationToken);

        return Unit.Value;
    }
}
