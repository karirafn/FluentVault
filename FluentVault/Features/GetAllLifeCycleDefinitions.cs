using System.Xml.Linq;

using FluentVault.Common;

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
    private readonly IVaultService _vaultService;

    public GetAllLifeCycleDefinitionsHandler(IVaultService vaultService)
    {
        _vaultService = vaultService;
        Serializer = new(_request);
    }

    public GetAllLifeCycleDefinitionsSerializer Serializer { get; }

    public async Task<IEnumerable<VaultLifeCycleDefinition>> Handle(GetAllLifeCycleDefinitionsQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _vaultService.SendAuthenticatedAsync(_request, cancellationToken: cancellationToken);
        IEnumerable<VaultLifeCycleDefinition> lifeCycles = Serializer.DeserializeMany(response);

        return lifeCycles;
    }

    internal class GetAllLifeCycleDefinitionsSerializer : XDocumentSerializer<VaultLifeCycleDefinition>
    {
        public GetAllLifeCycleDefinitionsSerializer(VaultRequest request)
            : base(request.Operation, new VaultLifeCycleDefinitionSerializer(request.Namespace)) { }
    }
}
