using System.Xml.Linq;

using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;

internal record GetAllLifeCycleDefinitionsQuery(VaultSessionCredentials Session) : IRequest<IEnumerable<VaultLifeCycleDefinition>>;

internal class GetAllLifeCycleDefinitionsHandler : IRequestHandler<GetAllLifeCycleDefinitionsQuery, IEnumerable<VaultLifeCycleDefinition>>
{
    private const string Operation = "GetAllLifeCycleDefinitions";

    private readonly IVaultRequestService _vaultRequestService;

    public GetAllLifeCycleDefinitionsHandler(IVaultRequestService vaultRequestService)
        => _vaultRequestService = vaultRequestService;

    public async Task<IEnumerable<VaultLifeCycleDefinition>> Handle(GetAllLifeCycleDefinitionsQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _vaultRequestService.SendAsync(Operation, query.Session);
        IEnumerable<VaultLifeCycleDefinition> lifeCycles = VaultLifeCycleDefinition.ParseAll(response);

        return lifeCycles;
    }
}
