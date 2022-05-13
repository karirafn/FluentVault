
using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.Search;
using FluentVault.Domain.Search.Items;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record FindItemRevisionsBySearchConditionsQuery(
    IEnumerable<SearchCondition> SearchConditions,
    IEnumerable<SortCondition>? SortConditions = null,
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
          @namespace: "Services/Item/1/7/2020");
    private readonly IMediator _mediator;
    private readonly IVaultService _vaultService;
    private readonly SearchConditionSerializer _searchConditionSerializer = new(_request.XNamespace);
    private readonly SortConditionSerializer _sortConditionSerializer = new(_request.XNamespace);

    public FindItemRevisionsBySearchConditionsHandler(IMediator mediator, IVaultService vaultService)
    {
        _mediator = mediator;
        _vaultService = vaultService;
        Serializer = new(_request);
    }

    public FindItemRevisionsBySearchConditionsSerializer Serializer { get; }

    public async Task<VaultSearchItemsResponse> Handle(FindItemRevisionsBySearchConditionsQuery query, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace @namespace) => content
            .AddNestedElements(@namespace, "searchConditions", _searchConditionSerializer.Serialize(query.SearchConditions))
            .AddNestedElements(@namespace, "sortConditions", _sortConditionSerializer.Serialize(query.SortConditions))
            .AddElement(@namespace, "bRequestLatestOnly", query.LatestOnly)
            .AddElement(@namespace, "bookmark", query.Bookmark);

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
