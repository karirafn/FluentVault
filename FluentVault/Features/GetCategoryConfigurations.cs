using System.Xml.Linq;

using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;

internal record GetCategoryConfigurationsQuery
    (VaultSessionCredentials Session) : IRequest<IEnumerable<VaultCategory>>;

internal class GetCategoryConfigurationsHandler : IRequestHandler<GetCategoryConfigurationsQuery, IEnumerable<VaultCategory>>
{
    private const string Operation = "GetCategoryConfigurationsByBehaviorNames";

    private readonly IVaultRequestService _vaultRequestService;

    public GetCategoryConfigurationsHandler(IVaultRequestService vaultRequestService)
        => _vaultRequestService = vaultRequestService;

    public async Task<IEnumerable<VaultCategory>> Handle(GetCategoryConfigurationsQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _vaultRequestService.SendAsync(Operation, query.Session);
        IEnumerable<VaultCategory> categories = VaultCategory.ParseAll(response);

        return categories;
    }
}
