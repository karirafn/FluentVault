using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.Search;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record SearchFilesCommand(
    IEnumerable<IDictionary<string, object>> SearchConditions,
    IEnumerable<IDictionary<string, object>> SortConditions,
    IEnumerable<VaultFolderId> FolderIds,
    bool RecurseFolders,
    bool LatestOnly,
    string Bookmark) : IRequest<VaultFileSearchResult>;

internal class SearchFilesHandler : IRequestHandler<SearchFilesCommand, VaultFileSearchResult>
{
    private const string Operation = "FindFilesBySearchConditions";

    private readonly IVaultService _vaultRequestService;

    public SearchFilesHandler(IVaultService vaultRequestService)
        => _vaultRequestService = vaultRequestService;

    public async Task<VaultFileSearchResult> Handle(SearchFilesCommand command, CancellationToken cancellationToken)
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
        var result = VaultFileSearchResult.Parse(response);

        return result;
    }
}
