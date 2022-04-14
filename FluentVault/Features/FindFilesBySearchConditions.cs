using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.Search.Files;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record FindFilesBySearchConditionsQuery(
    IEnumerable<IDictionary<string, object>> SearchConditions,
    IEnumerable<IDictionary<string, object>> SortConditions,
    IEnumerable<VaultFolderId> FolderIds,
    bool RecurseFolders,
    bool LatestOnly,
    string Bookmark) : IRequest<VaultSearchFilesResponse>;

internal class FindFilesBySearchConditionsHandler : IRequestHandler<FindFilesBySearchConditionsQuery, VaultSearchFilesResponse>
{
    private const string Operation = "FindFilesBySearchConditions";

    private readonly IVaultService _vaultRequestService;

    public FindFilesBySearchConditionsHandler(IVaultService vaultRequestService)
        => _vaultRequestService = vaultRequestService;

    public async Task<VaultSearchFilesResponse> Handle(FindFilesBySearchConditionsQuery command, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace ns)
        {
            content.AddNestedElementsWithAttributes(ns, "conditions", "SrchCond", command.SearchConditions);
            content.AddNestedElementsWithAttributes(ns, "sortConditions", "SrchSort", command.SortConditions);
            content.AddNestedElements(ns, "folderIds", "long", command.FolderIds.Select(id => id.ToString()));
            content.AddElement(ns, "recurseFolders", command.RecurseFolders);
            content.AddElement(ns, "latestOnly", command.LatestOnly);
            content.AddElement(ns, "bookmark", command.Bookmark);
        }

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
