using System.Xml.Linq;

using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;

internal record GetAllLifeCycleDefinitionsQuery(VaultSessionCredentials Session) : IRequest<IEnumerable<VaultLifeCycleDefinition>>;

internal class GetAllLifeCycleDefinitionsHandler : IRequestHandler<GetAllLifeCycleDefinitionsQuery, IEnumerable<VaultLifeCycleDefinition>>
{
    private const string Operation = "GetAllLifeCycleDefinitions";

    private readonly IVaultRequestService _soapRequestService;
    private readonly VaultSessionCredentials _session;

    public GetAllLifeCycleDefinitionsHandler(IVaultRequestService soapRequestService, VaultSessionCredentials session)
        => (_soapRequestService, _session) = (soapRequestService, session);

    public async Task<IEnumerable<VaultLifeCycleDefinition>> Handle(GetAllLifeCycleDefinitionsQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _soapRequestService.SendAsync(Operation, _session);
        IEnumerable<VaultLifeCycleDefinition> lifeCycles = VaultLifeCycleDefinition.ParseAll(response);

        return lifeCycles;
    }
}
