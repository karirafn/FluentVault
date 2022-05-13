using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.Search;
using FluentVault.Domain.Search.Files;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record FindFilesBySearchConditionsQuery(
    IEnumerable<SearchCondition> SearchConditions,
    IEnumerable<SortCondition>? SortConditions = null,
    IEnumerable<VaultFolderId>? FolderIds = null,
    bool RecurseFolders = true,
    bool LatestOnly = true,
    string Bookmark = "") : IRequest<VaultSearchFilesResponse>;
internal class FindFilesBySearchConditionsHandler : IRequestHandler<FindFilesBySearchConditionsQuery, VaultSearchFilesResponse>
{
    private static readonly VaultRequest _request = new(
          operation: "FindFilesBySearchConditions",
          version: "v26",
          service: "DocumentService",
          command: "",
          @namespace: "Services/Document/1/7/2020");
    private readonly IMediator _mediator;
    private readonly IVaultService _vaultService;
    private readonly SearchConditionSerializer _searchConditionSerializer = new(_request.XNamespace);
    private readonly SortConditionSerializer _sortConditionSerializer = new(_request.XNamespace);

    public FindFilesBySearchConditionsHandler(IMediator mediator, IVaultService vaultService)
    {
        _mediator = mediator;
        _vaultService = vaultService;
    }

    public FindFilesBySearchConditionsSerializer Serializer { get; } = new(_request);

    public async Task<VaultSearchFilesResponse> Handle(FindFilesBySearchConditionsQuery query, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace @namespace) => content
            .AddNestedElements(@namespace, "conditions", _searchConditionSerializer.Serialize(query.SearchConditions))
            .AddNestedElements(@namespace, "sortConditions", _sortConditionSerializer.Serialize(query.SortConditions))
            .AddNestedElements(@namespace, "folderIds", "long", query.FolderIds?.Select(id => id.ToString()))
            .AddElement(@namespace, "recurseFolders", query.RecurseFolders)
            .AddElement(@namespace, "latestOnly", query.LatestOnly)
            .AddElement(@namespace, "bookmark", query.Bookmark);

        XDocument response = await _mediator.SendAuthenticatedRequest(_request, _vaultService, contentBuilder, cancellationToken);
        VaultSearchFilesResponse result = Serializer.Deserialize(response);

        return result;
    }

    internal class FindFilesBySearchConditionsSerializer : XDocumentSerializer<VaultSearchFilesResponse>
    {
        public FindFilesBySearchConditionsSerializer(VaultRequest request)
            : base(request.Operation, new VaultSearchFilesResponseSerializer(request.Namespace)) { }
    }
}
