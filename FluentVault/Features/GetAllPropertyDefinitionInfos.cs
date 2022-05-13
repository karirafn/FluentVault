using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

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
    private readonly IMediator _mediator;
    private readonly IVaultService _vaultService;

    public GetAllPropertyDefinitionInfosHandler(IMediator mediator, IVaultService vaultService)
    {
        _mediator = mediator;
        _vaultService = vaultService;
    }

    public GetAllPropertyDefinitionInfosSerializer Serializer { get; } = new(_request);

    public async Task<IEnumerable<VaultProperty>> Handle(GetAllPropertyDefinitionInfosQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _mediator.SendAuthenticatedRequest(_request, _vaultService, null, cancellationToken);
        IEnumerable<VaultProperty> properties = Serializer.DeserializeMany(response);

        return properties;
    }

    internal class GetAllPropertyDefinitionInfosSerializer : XDocumentSerializer<VaultProperty>
    {
        public GetAllPropertyDefinitionInfosSerializer(VaultRequest request)
            : base(request.Operation, new VaultPropertySerializer(request.Namespace)) { }
    }
}
