using System.Xml.Linq;

using FluentVault.Domain;
using FluentVault.Domain.Lifecycle;
using FluentVault.Domain.SOAP;

using MediatR;

namespace FluentVault.Features;

internal class GetAllLifeCycleDefinitionsHandler : IRequestHandler<GetAllLifeCycleDefinitionsQuery, IEnumerable<VaultLifeCycle>>
{
    private const string RequestName = "GetAllLifeCycleDefinitions";

    private readonly ISoapRequestService _soapRequestService;
    private readonly VaultSessionCredentials _session;

    public GetAllLifeCycleDefinitionsHandler(ISoapRequestService soapRequestService, VaultSessionCredentials session)
    {
        _soapRequestService = soapRequestService;
        _session = session;
    }

    public async Task<IEnumerable<VaultLifeCycle>> Handle(GetAllLifeCycleDefinitionsQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _soapRequestService.SendAsync(RequestName, _session);
        IEnumerable<VaultLifeCycle> lifeCycles = response.ParseLifeCycles();

        return lifeCycles;
    }
}
