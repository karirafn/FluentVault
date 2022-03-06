﻿using System.Xml.Linq;

using FluentValidation;

using FluentVault.Common;
using FluentVault.Common.Extensions;
using FluentVault.Domain;

using MediatR;

namespace FluentVault.Features;
internal record SignInCommand(VaultOptions VaultOptions) : IRequest<VaultSessionCredentials>;

internal class SignInHandler : IRequestHandler<SignInCommand, VaultSessionCredentials>
{
    private const string Operation = "SignIn";

    private readonly IVaultRequestService _soapRequestService;

    public SignInHandler(IVaultRequestService soapRequestService)
        => _soapRequestService = soapRequestService;

    public async Task<VaultSessionCredentials> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        new VaultOptionsValidator().ValidateAndThrow(command.VaultOptions);

        void contentBuilder(XElement content, XNamespace ns)
        {
            content.AddElement(ns, "dataServer", $"http://{command.VaultOptions.Server}");
            content.AddElement(ns, "knowledgeVault", command.VaultOptions.Database);
            content.AddElement(ns, "userName", command.VaultOptions.Username);
            content.AddElement(ns, "userPassword", command.VaultOptions.Password);
        }

        XDocument document = await _soapRequestService.SendAsync(Operation, new(), contentBuilder);

        string t = document.GetElementValue("Ticket");
        string u = document.GetElementValue("UserId");

        var session = ParseSessionCredentials(t, u);

        return session;
    }

    private static VaultSessionCredentials ParseSessionCredentials(string t, string u)
    {
        if (Guid.TryParse(t, out Guid ticket) == false)
            throw new ArgumentException($@"Failed to parse ticket with value ""{t}"".");

        if (long.TryParse(u, out long userId) == false)
            throw new ArgumentException($@"Failed to parse user ID with value ""{u}"".");

        return new VaultSessionCredentials(ticket, userId);
    }
}
