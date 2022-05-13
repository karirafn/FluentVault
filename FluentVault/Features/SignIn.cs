using System.Xml.Linq;

using FluentValidation;

using FluentVault.Common;
using FluentVault.Domain.SecurityHeader;
using FluentVault.Extensions;

using MediatR;

using Microsoft.Extensions.Options;

namespace FluentVault.Features;
internal record SignInCommand() : IRequest<VaultSecurityHeader>;
internal class SignInHandler : IRequestHandler<SignInCommand, VaultSecurityHeader>
{
    private static readonly VaultRequest _request = new(
          operation: "SignIn",
          version: "Filestore/v26_2",
          service: "AuthService",
          command: "Connectivity.Application.VaultBase.SignInCommand",
          @namespace: "Filestore/Auth/1/8/2021");
    private readonly IVaultService _vaultService;
    private readonly VaultOptions _options;

    public SignInHandler(IVaultService vaultService, IOptions<VaultOptions> options)
    {
        _vaultService = vaultService;
        _options = options.Value;
    }

    public SignInSerializer Serializer { get; } = new(_request);

    public async Task<VaultSecurityHeader> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        new VaultOptionsValidator().ValidateAndThrow(_options);

        void contentBuilder(XElement content, XNamespace ns) => content
            .AddElement(ns, "dataServer", $"http://{_options.Server}")
            .AddElement(ns, "knowledgeVault", _options.Database)
            .AddElement(ns, "userName", _options.Username)
            .AddElement(ns, "userPassword", _options.Password);

        XDocument requestBody = VaultRequestSerializer.Serialize(_request, null, contentBuilder);
        XDocument document = await _vaultService.SendAsync(_request.Uri, _request.SoapAction, requestBody, cancellationToken: cancellationToken);
        VaultSecurityHeader result = Serializer.Deserialize(document);

        return result;
    }

    internal class SignInSerializer : XDocumentSerializer<VaultSecurityHeader>
    {
        public SignInSerializer(VaultRequest request)
            : base(request.Operation, new VaultSecurityHeaderSerializer()) { }
    }
}
