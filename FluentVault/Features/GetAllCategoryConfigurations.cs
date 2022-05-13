using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

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
    private readonly IMediator _mediator;
    private readonly IVaultService _vaultService;

    public GetAllCategoryConfigurationsHandler(IMediator mediator, IVaultService vaultService)
    {
        _mediator = mediator;
        _vaultService = vaultService;
    }

    public GetCategoryConfigurationsByBehaviorNamesSerializer Serializer { get; } = new(_request);

    public async Task<IEnumerable<VaultCategory>> Handle(GetAllCategoryConfigurationsQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _mediator.SendAuthenticatedRequest(_request, _vaultService, null, cancellationToken);
        IEnumerable<VaultCategory> categories = Serializer.DeserializeMany(response);

        return categories;
    }

    internal class GetCategoryConfigurationsByBehaviorNamesSerializer : XDocumentSerializer<VaultCategory>
    {
        public GetCategoryConfigurationsByBehaviorNamesSerializer(VaultRequest request)
            : base(request.Operation, new VaultCategorySerializer(request.Namespace)) { }
    }
}
