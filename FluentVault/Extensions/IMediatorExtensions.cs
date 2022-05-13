
using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.SecurityHeader;
using FluentVault.Features;

using MediatR;

namespace FluentVault.Extensions;
internal static class IMediatorExtensions
{
    public static async Task<XDocument> SendAuthenticatedRequest(this IMediator mediator,
        VaultRequest request,
        IVaultService vaultService,
        Action<XElement, XNamespace>? contentBuilder = null,
        CancellationToken cancellationToken = default)
    {
        VaultSecurityHeader securityHeader = await mediator.SignIn(cancellationToken: cancellationToken);
        XDocument requestBody = VaultRequestSerializer.Serialize(request, securityHeader, contentBuilder);
        XDocument response = await vaultService.SendAsync(request.Uri, request.SoapAction, requestBody, cancellationToken);

        await mediator.SignOut(securityHeader, cancellationToken);

        return response;
    }

    public static async Task<VaultSecurityHeader> SignIn(this IMediator mediator, CancellationToken cancellationToken = default)
        => await mediator.Send(new SignInCommand(), cancellationToken);

    public static async Task SignOut(this IMediator mediator, VaultSecurityHeader securityHeader, CancellationToken cancellationToken = default)
        => _ = await mediator.Send(new SignOutCommand(securityHeader), cancellationToken);
}
