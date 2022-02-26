
using System.Text;
using System.Xml.Linq;

using FluentValidation;

using FluentVault.Common.Extensions;
using FluentVault.Domain;
using FluentVault.Domain.SOAP;

using MediatR;

namespace FluentVault.Features;

internal class SignInHandler : IRequestHandler<SignInCommand, VaultSessionCredentials>
{
    private const string RequestName = "SignIn";

    private readonly ISoapRequestService _soapRequestService;

    public SignInHandler(ISoapRequestService soapRequestService)
    {
        _soapRequestService = soapRequestService;
    }

    public async Task<VaultSessionCredentials> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var validator = new VaultOptionsValidator();
        var results = validator.Validate(command.VaultOptions, options => options.ThrowOnFailures());

        string requestBody = GenerateRequestBody(command.VaultOptions);
        XDocument document = await _soapRequestService.SendAsync(RequestName, requestBody);

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

    private string GenerateRequestBody(VaultOptions o)
        => new StringBuilder()
            .AppendRequestBodyOpening(new())
            .AppendElementWithAttribute(RequestName, "xmlns", _soapRequestService.GetNamespace(RequestName))
            .AppendElement("dataServer", $"http://{o.Server}")
            .AppendElement("knowledgeVault", o.Database)
            .AppendElement("userName", o.Username)
            .AppendElement("userPassword", o.Password)
            .AppendElementClosing(RequestName)
            .AppendRequestBodyClosing()
            .ToString();
}
