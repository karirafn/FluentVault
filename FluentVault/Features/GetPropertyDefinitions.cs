using System.Xml.Linq;

using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;

internal record GetPropertyDefinitionsQuery(VaultSessionCredentials Session) : IRequest<IEnumerable<VaultPropertyDefinition>>;

internal class GetPropertyDefinitionsHandler : IRequestHandler<GetPropertyDefinitionsQuery, IEnumerable<VaultPropertyDefinition>>
{
    private const string Operation = "GetPropertyDefinitionInfosByEntityClassId";

    private readonly IVaultRequestService _soapRequestService;
    private readonly VaultSessionCredentials _session;

    public GetPropertyDefinitionsHandler(IVaultRequestService soapRequestService, VaultSessionCredentials session)
        => (_soapRequestService, _session) = (soapRequestService, session);

    public async Task<IEnumerable<VaultPropertyDefinition>> Handle(GetPropertyDefinitionsQuery request, CancellationToken cancellationToken)
    {
        XDocument response = await _soapRequestService.SendAsync(Operation, _session);
        IEnumerable<VaultPropertyDefinition> properties = VaultPropertyDefinition.ParseAll(response);

        return properties;
    }
}
