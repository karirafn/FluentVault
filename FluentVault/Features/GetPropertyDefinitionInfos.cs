using System.Xml.Linq;

using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;

internal record GetPropertyDefinitionInfosQuery(VaultSessionCredentials Session) : IRequest<IEnumerable<VaultProperty>>;

internal class GetPropertyDefinitionInfosHandler : IRequestHandler<GetPropertyDefinitionInfosQuery, IEnumerable<VaultProperty>>
{
    private const string Operation = "GetPropertyDefinitionInfosByEntityClassId";

    private readonly IVaultRequestService _vaultRequestService;
    private readonly VaultSessionCredentials _session;

    public GetPropertyDefinitionInfosHandler(IVaultRequestService vaultRequestService, VaultSessionCredentials session)
        => (_vaultRequestService, _session) = (vaultRequestService, session);

    public async Task<IEnumerable<VaultProperty>> Handle(GetPropertyDefinitionInfosQuery request, CancellationToken cancellationToken)
    {
        XDocument response = await _vaultRequestService.SendAsync(Operation, _session);
        IEnumerable<VaultProperty> properties = VaultProperty.ParseAll(response);

        return properties;
    }
}
