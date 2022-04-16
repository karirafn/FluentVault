
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
    private const string Operation = "FindItemRevisionsBySearchConditions";

    private readonly IVaultService _vaultService;

    public FindItemRevisionsBySearchConditionsHandler(IVaultService vaultService)
    {
        _vaultService = vaultService;
    }

    public async Task<VaultSearchItemsResponse> Handle(FindItemRevisionsBySearchConditionsQuery query, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace ns)
            => content.AddNestedElementsWithAttributes(ns, "conditions", "SrchCond", query.SearchConditions)
                .AddNestedElementsWithAttributes(ns, "sortConditions", "SrchSort", query.SortConditions)
                .AddNestedElements(ns, "folderIds", "long", query.FolderIds?.Select(id => id.ToString()))
                .AddElement(ns, "recurseFolders", query.RecurseFolders)
                .AddElement(ns, "latestOnly", query.LatestOnly)
                .AddElement(ns, "bookmark", query.Bookmark);

        XDocument response = await _vaultService.SendAsync(Operation, canSignIn: true, contentBuilder, cancellationToken);
        VaultSearchItemsResponse result = new FindItemRevisionsBySearchConditionsSerializer().Deserialize(response);

        return result;
    }
}

internal class FindItemRevisionsBySearchConditionsSerializer : XDocumentSerializer<VaultSearchItemsResponse>
{
    private const string FindItemRevisionsBySearchConditions = nameof(FindItemRevisionsBySearchConditions);
    private static readonly VaultRequest _request = new VaultRequestData().Get(FindItemRevisionsBySearchConditions);

    public FindItemRevisionsBySearchConditionsSerializer() : base(_request.Operation, new VaultSearchItemsResponseSerializer(_request.Namespace)) { }
}
