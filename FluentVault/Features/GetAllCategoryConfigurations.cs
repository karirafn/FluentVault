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
        IEnumerable<VaultCategory> categories = new GetCategoryConfigurationsByBehaviorNamesSerializer().DeserializeMany(response);

        return categories;
    }
}

internal class GetCategoryConfigurationsByBehaviorNamesSerializer : XDocumentSerializer<VaultCategory>
{
    private const string GetCategoryConfigurationsByBehaviorNames = nameof(GetCategoryConfigurationsByBehaviorNames);
    private static readonly VaultRequest _request = new VaultRequestData().Get(GetCategoryConfigurationsByBehaviorNames);

    public GetCategoryConfigurationsByBehaviorNamesSerializer() : base(_request.Operation, new VaultCategorySerializer(_request.Namespace)) { }
}
