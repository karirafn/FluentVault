using System.Xml.Linq;

using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;
internal record GetAllPropertyDefinitionInfosQuery() : IRequest<IEnumerable<VaultProperty>>;
internal class GetAllPropertyDefinitionInfosHandler : IRequestHandler<GetAllPropertyDefinitionInfosQuery, IEnumerable<VaultProperty>>
{
    private static readonly VaultRequest _request = new(
          operation: "GetPropertyDefinitionInfosByEntityClassId",
          version: "v26",
          service: "PropertyService",
          command: "Connectivity.Explorer.Admin.AdminToolsCommand",
          @namespace: "Services/Property/1/7/2020");
    private readonly IVaultService _vaultService;

    public GetAllPropertyDefinitionInfosHandler(IVaultService vaultService)
    {
        _vaultService = vaultService;
        Serializer = new(_request);
    }

    public GetAllPropertyDefinitionInfosSerializer Serializer { get; }

    public async Task<IEnumerable<VaultProperty>> Handle(GetAllPropertyDefinitionInfosQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _vaultService.SendAuthenticatedAsync(_request, cancellationToken: cancellationToken);
        IEnumerable<VaultProperty> properties = Serializer.DeserializeMany(response);

        return properties;
    }

    internal class GetAllPropertyDefinitionInfosSerializer : XDocumentSerializer<VaultProperty>
    {
        public GetAllPropertyDefinitionInfosSerializer(VaultRequest request)
            : base(request.Operation, new VaultPropertySerializer(request.Namespace)) { }
    }
}
