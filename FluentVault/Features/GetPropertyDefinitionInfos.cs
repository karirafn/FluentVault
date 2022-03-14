using System.Xml.Linq;

using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;

internal record GetPropertyDefinitionInfosQuery(VaultSessionCredentials Session) : IRequest<IEnumerable<VaultProperty>>;

internal class GetPropertyDefinitionInfosHandler : IRequestHandler<GetPropertyDefinitionInfosQuery, IEnumerable<VaultProperty>>
{
    private const string Operation = "GetPropertyDefinitionInfosByEntityClassId";

    private readonly IVaultRequestService _vaultRequestService;

    public GetPropertyDefinitionInfosHandler(IVaultRequestService vaultRequestService)
        => _vaultRequestService = vaultRequestService;

    public async Task<IEnumerable<VaultProperty>> Handle(GetPropertyDefinitionInfosQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _vaultRequestService.SendAsync(Operation, query.Session, cancellationToken: cancellationToken);
        IEnumerable<VaultProperty> properties = VaultProperty.ParseAll(response);

        return properties;
    }
}
