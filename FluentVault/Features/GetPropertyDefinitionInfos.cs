using System.Xml.Linq;

using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;

internal record GetPropertyDefinitionInfosQuery(VaultSessionCredentials Session) : IRequest<IEnumerable<VaultProperty>>;

internal class GetPropertyDefinitionInfosHandler : IRequestHandler<GetPropertyDefinitionInfosQuery, IEnumerable<VaultProperty>>
{
    private const string Operation = "GetPropertyDefinitionInfosByEntityClassId";

    private readonly IVaultRequestService _soapRequestService;
    private readonly VaultSessionCredentials _session;

    public GetPropertyDefinitionInfosHandler(IVaultRequestService soapRequestService, VaultSessionCredentials session)
        => (_soapRequestService, _session) = (soapRequestService, session);

    public async Task<IEnumerable<VaultProperty>> Handle(GetPropertyDefinitionInfosQuery request, CancellationToken cancellationToken)
    {
        XDocument response = await _soapRequestService.SendAsync(Operation, _session);
        IEnumerable<VaultProperty> properties = VaultProperty.ParseAll(response);

        return properties;
    }
}
