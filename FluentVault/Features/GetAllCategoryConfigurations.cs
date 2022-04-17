using System.Xml.Linq;

using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;
internal record GetAllCategoryConfigurationsQuery() : IRequest<IEnumerable<VaultCategory>>;
internal class GetAllCategoryConfigurationsHandler : IRequestHandler<GetAllCategoryConfigurationsQuery, IEnumerable<VaultCategory>>
{
    private static readonly VaultRequest _request = new(
          operation: "GetCategoryConfigurationsByBehaviorNames",
          version: "v26",
          service: "CategoryService",
          command: "Connectivity.Explorer.Admin.AdminToolsCommand",
          @namespace: "Services/Category/1/7/2020");
    private readonly IVaultService _vaultService;

    public GetAllCategoryConfigurationsHandler(IVaultService vaultService)
    {
        _vaultService = vaultService;
        Serializer = new(_request);
    }

    public GetCategoryConfigurationsByBehaviorNamesSerializer Serializer { get; }

    public async Task<IEnumerable<VaultCategory>> Handle(GetAllCategoryConfigurationsQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _vaultService.SendAsync(_request, canSignIn: true, cancellationToken: cancellationToken);
        IEnumerable<VaultCategory> categories = Serializer.DeserializeMany(response);

        return categories;
    }

    internal class GetCategoryConfigurationsByBehaviorNamesSerializer : XDocumentSerializer<VaultCategory>
    {
        public GetCategoryConfigurationsByBehaviorNamesSerializer(VaultRequest request)
            : base(request.Operation, new VaultCategorySerializer(request.Namespace)) { }
    }
}
