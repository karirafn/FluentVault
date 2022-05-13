
using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.Search.Items;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record FindItemRevisionsBySearchConditionsQuery(
    IEnumerable<IDictionary<string, object>> SearchConditions,
    IEnumerable<IDictionary<string, object>>? SortConditions = null,
    IEnumerable<VaultFolderId>? FolderIds = null,
    bool RecurseFolders = true,
    bool LatestOnly = true,
    string Bookmark = "") : IRequest<VaultSearchItemsResponse>;
internal class FindItemRevisionsBySearchConditionsHandler : IRequestHandler<FindItemRevisionsBySearchConditionsQuery, VaultSearchItemsResponse>
{
    private static readonly VaultRequest _request = new(
          operation: "FindItemRevisionsBySearchConditions",
          version: "v26",
          service: "ItemService",
          command: "",
          @namespace: "Services/Document/1/7/2020");
    private readonly IMediator _mediator;
    private readonly IVaultService _vaultService;

    public FindItemRevisionsBySearchConditionsHandler(IMediator mediator, IVaultService vaultService)
    {
        _mediator = mediator;
        _vaultService = vaultService;
        Serializer = new(_request);
    }

    public FindItemRevisionsBySearchConditionsSerializer Serializer { get; }

    public async Task<VaultSearchItemsResponse> Handle(FindItemRevisionsBySearchConditionsQuery query, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace ns) => content
            .AddNestedElementsWithAttributes(ns, "conditions", "SrchCond", query.SearchConditions)
            .AddNestedElementsWithAttributes(ns, "sortConditions", "SrchSort", query.SortConditions)
            .AddNestedElements(ns, "folderIds", "long", query.FolderIds?.Select(id => id.ToString()))
            .AddElement(ns, "recurseFolders", query.RecurseFolders)
            .AddElement(ns, "latestOnly", query.LatestOnly)
            .AddElement(ns, "bookmark", query.Bookmark);

        XDocument response = await _mediator.SendAuthenticatedRequest(_request, _vaultService, contentBuilder, cancellationToken);
        VaultSearchItemsResponse result = Serializer.Deserialize(response);

        return result;
    }

    internal class FindItemRevisionsBySearchConditionsSerializer : XDocumentSerializer<VaultSearchItemsResponse>
    {
        public FindItemRevisionsBySearchConditionsSerializer(VaultRequest request)
            : base(request.Operation, new VaultSearchItemsResponseSerializer(request.Namespace)) { }
    }
}
