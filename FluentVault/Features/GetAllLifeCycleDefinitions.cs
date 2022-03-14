using System.Xml.Linq;

using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;

internal record GetAllLifeCycleDefinitionsQuery(VaultSessionCredentials Session) : IRequest<IEnumerable<VaultLifeCycleDefinition>>;

internal class GetAllLifeCycleDefinitionsHandler : IRequestHandler<GetAllLifeCycleDefinitionsQuery, IEnumerable<VaultLifeCycleDefinition>>
{
    private const string Operation = "GetAllLifeCycleDefinitions";

    private readonly IVaultRequestService _vaultRequestService;
    private readonly VaultSessionCredentials _session;

    public GetAllLifeCycleDefinitionsHandler(IVaultRequestService vaultRequestService, VaultSessionCredentials session)
        => (_vaultRequestService, _session) = (vaultRequestService, session);

    public async Task<IEnumerable<VaultLifeCycleDefinition>> Handle(GetAllLifeCycleDefinitionsQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _vaultRequestService.SendAsync(Operation, _session);
        IEnumerable<VaultLifeCycleDefinition> lifeCycles = VaultLifeCycleDefinition.ParseAll(response);

        return lifeCycles;
    }
}
