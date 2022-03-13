using System.Xml.Linq;

using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;

internal record GetCategoryConfigurationsQuery
    (VaultSessionCredentials Session) : IRequest<IEnumerable<VaultCategoryConfiguration>>;

internal class GetCategoryConfigurationsHandler : IRequestHandler<GetCategoryConfigurationsQuery, IEnumerable<VaultCategoryConfiguration>>
{
    private const string Operation = "GetCategoryConfigurationsByBehaviorNames";

    private readonly IVaultRequestService _soapRequestService;
    private readonly VaultSessionCredentials _session;

    public GetCategoryConfigurationsHandler(IVaultRequestService soapRequestService, VaultSessionCredentials session)
        => (_soapRequestService, _session) = (soapRequestService, session);

    public async Task<IEnumerable<VaultCategoryConfiguration>> Handle(GetCategoryConfigurationsQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _soapRequestService.SendAsync(Operation, _session);
        IEnumerable<VaultCategoryConfiguration> categories = VaultCategoryConfiguration.ParseAll(response);

        return categories;
    }
}
