using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.Search;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record SearchFilesCommand(
    IEnumerable<IDictionary<string, string>> SearchConditions,
    IEnumerable<IDictionary<string, string>> SortConditions,
    IEnumerable<VaultFolderId> FolderIds,
    bool RecurseFolders,
    bool LatestOnly,
    string Bookmark,
    VaultSessionCredentials Session) : IRequest<FileSearchResult>;

internal class SearchFilesHandler : IRequestHandler<SearchFilesCommand, FileSearchResult>
{
    private const string Operation = "FindFilesBySearchConditions";

    private readonly IVaultRequestService _vaultRequestService;

    public SearchFilesHandler(IVaultRequestService vaultRequestService)
        => _vaultRequestService = vaultRequestService;

    public async Task<FileSearchResult> Handle(SearchFilesCommand command, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace ns)
        {
            content.AddElementsWithAttributes(ns, "SrchCond", command.SearchConditions);
            content.AddElementsWithAttributes(ns, "SrchSort", command.SortConditions);
            content.AddNestedElements(ns, "folderIds", "long", command.FolderIds.Select(id => id.ToString()));
            content.AddElement(ns, "recurseFolders", command.RecurseFolders);
            content.AddElement(ns, "latestOnly", command.LatestOnly);
            content.AddElement(ns, "bookmark", command.Bookmark);
        }

        XDocument response = await _vaultRequestService.SendAsync(Operation, command.Session, contentBuilder, cancellationToken);
        var result = FileSearchResult.Parse(response);

        return result;
    }
}
