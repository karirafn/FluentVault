using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.PropertyInstance;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record GetPropertiesQuery(VaultEntityClass EntityClass, IEnumerable<VaultEntityId> EntityIds, IEnumerable<VaultPropertyDefinitionId> PropertyIds) : IRequest<IEnumerable<VaultPropertyInstance>>;
internal class GetPropertiesHandler : IRequestHandler<GetPropertiesQuery, IEnumerable<VaultPropertyInstance>>
{
    private static readonly VaultRequest _request = new(
          operation: "GetProperties",
          version: "v26",
          service: "PropertyService",
          command: "",
          @namespace: "Services/Property/1/7/2020");
    private readonly IMediator _mediator;
    private readonly IVaultService _vaultService;

    public GetPropertiesHandler(IMediator mediator, IVaultService vaultService)
    {
        _mediator = mediator;
        _vaultService = vaultService;
    }

    public GetPropertiesSerializer Serializer { get; } = new(_request);

    public async Task<IEnumerable<VaultPropertyInstance>> Handle(GetPropertiesQuery query, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace @namespace) => content
            .AddElement(@namespace, "entityClassId", query.EntityClass)
            .AddNestedElements(@namespace, "entityIds", "long", query.EntityIds.Select(id => id.ToString()))
            .AddNestedElements(@namespace, "propertyDefIds", "long", query.PropertyIds.Select(id => id.ToString()));

        XDocument response = await _mediator.SendAuthenticatedRequest(_request, _vaultService, contentBuilder, cancellationToken);
        IEnumerable<VaultPropertyInstance> result = Serializer.DeserializeMany(response);

        return result;
    }

    internal class GetPropertiesSerializer : XDocumentSerializer<VaultPropertyInstance>
    {
        public GetPropertiesSerializer(VaultRequest request)
            : base(request.Operation, new VaultPropertyInstanceSerializer(request.Namespace)) { }
    }
}
