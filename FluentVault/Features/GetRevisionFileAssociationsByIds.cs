using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record GetRevisionFileAssociationsByIdsQuery(
    IEnumerable<VaultFileIterationId> Ids,
    VaultFileAssociationType ParentAssociationType,
    bool RecurseParents,
    VaultFileAssociationType ChildAssociationType,
    bool RecurseChildren,
    bool IncludeRelatedDocumentation,
    bool IncludeHidden,
    bool ReleasedBiased) : IRequest<IEnumerable<VaultFileAssociation>>;
internal class GetRevisionFileAssociationsByIdsHandler : IRequestHandler<GetRevisionFileAssociationsByIdsQuery, IEnumerable<VaultFileAssociation>>
{
    private static readonly VaultRequest _request = new(
          operation: "GetRevisionFileAssociationsByIds",
          version: "v26",
          service: "DocumentService",
          command: "",
          @namespace: "Services/Document/1/7/2020");
    private readonly IMediator _mediator;
    private readonly IVaultService _vaultService;

    public GetRevisionFileAssociationsByIdsHandler(IMediator mediator, IVaultService vaultService)
    {
        _mediator = mediator;
        _vaultService = vaultService;
    }

    public GetRevisionFileAssociationsByIdsSerializer Serializer { get; } = new(_request);

    public async Task<IEnumerable<VaultFileAssociation>> Handle(GetRevisionFileAssociationsByIdsQuery query, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace @namespace) => content
            .AddNestedElements(@namespace, "fileIds", "long", query.Ids?.Select(id => id.ToString()))
            .AddElement(@namespace, "parentAssociationType", query.ParentAssociationType)
            .AddElement(@namespace, "parentRecurse", query.RecurseParents)
            .AddElement(@namespace, "childAssociationType", query.ChildAssociationType)
            .AddElement(@namespace, "childRecurse", query.RecurseChildren)
            .AddElement(@namespace, "includeRelatedDocuments", query.IncludeRelatedDocumentation)
            .AddElement(@namespace, "includeHidden", query.IncludeHidden)
            .AddElement(@namespace, "releasedBiased", query.ReleasedBiased);

        XDocument response = await _mediator.SendAuthenticatedRequest(_request, _vaultService, contentBuilder, cancellationToken);
        IEnumerable<VaultFileAssociation> result = Serializer.DeserializeMany(response);

        return result;
    }

    internal class GetRevisionFileAssociationsByIdsSerializer : XDocumentSerializer<VaultFileAssociation>
    {
        public GetRevisionFileAssociationsByIdsSerializer(VaultRequest request)
            : base(request.Operation, new VaultFileAssociationSerializer(request.Namespace)) { }
    }
}
