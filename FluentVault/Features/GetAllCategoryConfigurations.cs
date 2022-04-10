using System.Xml.Linq;

using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;

internal record GetAllCategoryConfigurationsQuery() : IRequest<IEnumerable<VaultCategory>>;

internal class GetAllCategoryConfigurationsHandler : IRequestHandler<GetAllCategoryConfigurationsQuery, IEnumerable<VaultCategory>>
{
    private const string Operation = "GetCategoryConfigurationsByBehaviorNames";

    private readonly IVaultService _vaultRequestService;

    public GetAllCategoryConfigurationsHandler(IVaultService vaultRequestService)
        => _vaultRequestService = vaultRequestService;

    public async Task<IEnumerable<VaultCategory>> Handle(GetAllCategoryConfigurationsQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _vaultRequestService.SendAsync(Operation, canSignIn: true, cancellationToken: cancellationToken);
        IEnumerable<VaultCategory> categories = VaultCategory.ParseAll(response);

        return categories;
    }
}
