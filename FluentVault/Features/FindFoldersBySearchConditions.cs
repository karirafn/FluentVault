using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.Search;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record FindFoldersBySearchConditionsQuery(
    IEnumerable<IDictionary<string, object>> SearchConditions,
    IEnumerable<IDictionary<string, object>> SortConditions,
    IEnumerable<VaultFolderId> FolderIds,
    bool RecurseFolders,
    bool LatestOnly,
    string Bookmark) : IRequest<VaultSearchFoldersResponse>;

internal class FindFoldersBySearchConditionsHandler : IRequestHandler<FindFoldersBySearchConditionsQuery, VaultSearchFoldersResponse>
{
    private const string Operation = "FindFoldersBySearchConditions";

    private readonly IVaultService _vaultRequestService;

    public FindFoldersBySearchConditionsHandler(IVaultService vaultRequestService)
        => _vaultRequestService = vaultRequestService;

    public async Task<VaultSearchFoldersResponse> Handle(FindFoldersBySearchConditionsQuery command, CancellationToken cancellationToken)
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
        VaultSearchFoldersResponse result = new FindFoldersBySearchConditionsSerializer().Deserialize(response);

        return result;
    }
}

internal class FindFoldersBySearchConditionsSerializer : XDocumentSerializer<VaultSearchFoldersResponse>
{
    private const string FindFoldersBySearchConditions = nameof(FindFoldersBySearchConditions);
    private static readonly VaultRequest _request = new VaultRequestData().Get(FindFoldersBySearchConditions);

    public FindFoldersBySearchConditionsSerializer() : base(_request.Operation, new VaultSearchFoldersResponseSerializer(_request.Namespace)) { }
}
