using System.Xml.Linq;

using FluentVault.Domain;
using FluentVault.Domain.Property;
using FluentVault.Domain.SOAP;

using MediatR;

namespace FluentVault.Features;

internal record GetPropertyDefinitionsQuery(VaultSessionCredentials Session) : IRequest<IEnumerable<VaultPropertyDefinition>>;

internal class GetPropertyDefinitionsHandler : IRequestHandler<GetPropertyDefinitionsQuery, IEnumerable<VaultPropertyDefinition>>
{
    private const string Operation = "GetPropertyDefinitionInfosByEntityClassId";

    private readonly ISoapRequestService _soapRequestService;
    private readonly VaultSessionCredentials _session;

    public GetPropertyDefinitionsHandler(ISoapRequestService soapRequestService, VaultSessionCredentials session)
    {
        _soapRequestService = soapRequestService;
        _session = session;
    }

    public async Task<IEnumerable<VaultPropertyDefinition>> Handle(GetPropertyDefinitionsQuery request, CancellationToken cancellationToken)
    {
        XDocument response = await _soapRequestService.SendAsync(Operation, _session);
        IEnumerable<VaultPropertyDefinition> properties = response.ParseProperties();

        return properties;
    }
}
