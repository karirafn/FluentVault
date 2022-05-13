using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record GetAllLifeCycleDefinitionsQuery() : IRequest<IEnumerable<VaultLifeCycleDefinition>>;
internal class GetAllLifeCycleDefinitionsHandler : IRequestHandler<GetAllLifeCycleDefinitionsQuery, IEnumerable<VaultLifeCycleDefinition>>
{
    private static readonly VaultRequest _request = new(
          operation: "GetAllLifeCycleDefinitions",
          version: "v26",
          service: "LifeCycleService",
          command: "Connectivity.Explorer.Admin.AdminToolsCommand",
          @namespace: "Services/LifeCycle/1/7/2020");
    private readonly IMediator _mediator;
    private readonly IVaultService _vaultService;

    public GetAllLifeCycleDefinitionsHandler(IMediator mediator, IVaultService vaultService)
    {
        _mediator = mediator;
        _vaultService = vaultService;
    }

    public GetAllLifeCycleDefinitionsSerializer Serializer { get; } = new(_request);

    public async Task<IEnumerable<VaultLifeCycleDefinition>> Handle(GetAllLifeCycleDefinitionsQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _mediator.SendAuthenticatedRequest(_request, _vaultService, null, cancellationToken);
        IEnumerable<VaultLifeCycleDefinition> lifeCycles = Serializer.DeserializeMany(response);

        return lifeCycles;
    }

    internal class GetAllLifeCycleDefinitionsSerializer : XDocumentSerializer<VaultLifeCycleDefinition>
    {
        public GetAllLifeCycleDefinitionsSerializer(VaultRequest request)
            : base(request.Operation, new VaultLifeCycleDefinitionSerializer(request.Namespace)) { }
    }
}
