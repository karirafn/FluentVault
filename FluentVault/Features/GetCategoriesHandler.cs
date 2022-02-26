using System.Xml.Linq;

using FluentVault.Domain;
using FluentVault.Domain.Category;
using FluentVault.Domain.SOAP;

using MediatR;

namespace FluentVault.Features;

internal class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<VaultCategory>>
{
    private const string RequestName = "GetCategoryConfigurationsByBehaviorNames";

    private readonly ISoapRequestService _soapRequestService;
    private readonly VaultSessionCredentials _session;

    public GetCategoriesHandler(ISoapRequestService soapRequestService, VaultSessionCredentials session)
    {
        _soapRequestService = soapRequestService;
        _session = session;
    }

    public async Task<IEnumerable<VaultCategory>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _soapRequestService.SendAsync(RequestName, _session);
        IEnumerable<VaultCategory> categories = response.ParseCategories();

        return categories;
    }
}
