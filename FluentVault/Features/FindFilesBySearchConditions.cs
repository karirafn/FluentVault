using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.Search.Files;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record FindFilesBySearchConditionsQuery(
    IEnumerable<IDictionary<string, object>> SearchConditions,
    IEnumerable<IDictionary<string, object>>? SortConditions = null,
    IEnumerable<VaultFolderId>? FolderIds = null,
    bool RecurseFolders = true,
    bool LatestOnly = true,
    string Bookmark = "") : IRequest<VaultSearchFilesResponse>;

internal class FindFilesBySearchConditionsHandler : IRequestHandler<FindFilesBySearchConditionsQuery, VaultSearchFilesResponse>
{
    private const string Operation = "FindFilesBySearchConditions";

    private readonly IVaultService _vaultRequestService;

    public FindFilesBySearchConditionsHandler(IVaultService vaultRequestService)
        => _vaultRequestService = vaultRequestService;

    public async Task<VaultSearchFilesResponse> Handle(FindFilesBySearchConditionsQuery command, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace ns)
            => content.AddNestedElementsWithAttributes(ns, "conditions", "SrchCond", command.SearchConditions)
                .AddNestedElementsWithAttributes(ns, "sortConditions", "SrchSort", command.SortConditions)
                .AddNestedElements(ns, "folderIds", "long", command.FolderIds?.Select(id => id.ToString()))
                .AddElement(ns, "recurseFolders", command.RecurseFolders)
                .AddElement(ns, "latestOnly", command.LatestOnly)
                .AddElement(ns, "bookmark", command.Bookmark);

        XDocument response = await _vaultRequestService.SendAsync(Operation, canSignIn: true, contentBuilder, cancellationToken);
        VaultSearchFilesResponse result = new FindFilesBySearchConditionsSerializer().Deserialize(response);

        return result;
    }
}

internal class FindFilesBySearchConditionsSerializer : XDocumentSerializer<VaultSearchFilesResponse>
{
    private const string FindFilesBySearchConditions = nameof(FindFilesBySearchConditions);
    private static readonly VaultRequest _request = new VaultRequestData().Get(FindFilesBySearchConditions);

    public FindFilesBySearchConditionsSerializer() : base(_request.Operation, new VaultSearchFilesResponseSerializer(_request.Namespace)) { }
}
