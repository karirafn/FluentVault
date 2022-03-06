﻿
using FluentVault.Domain;
using FluentVault.Domain.SOAP;

using MediatR;

namespace FluentVault.Features;

internal record SignOutCommand() : IRequest;

internal class SignOutHandler : IRequestHandler<SignOutCommand>
{
    private const string Operation = "SignOut";

    private readonly ISoapRequestService _soapRequestService;
    private readonly VaultSessionCredentials _session;

    public SignOutHandler(ISoapRequestService soapRequestService, VaultSessionCredentials session)
        => (_soapRequestService, _session) = (soapRequestService, session);

    public async Task<Unit> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        _ = await _soapRequestService.SendAsync(Operation, _session);

        return Unit.Value;
    }
}
