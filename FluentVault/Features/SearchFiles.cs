using System.Xml.Linq;

using FluentVault.Common.Extensions;
using FluentVault.Domain;
using FluentVault.Domain.Search;
using FluentVault.Domain.SOAP;

using MediatR;

namespace FluentVault.Features;
internal record SearchFilesCommand(
    IEnumerable<IDictionary<string, string>> SearchConditions,
    IEnumerable<IDictionary<string, string>> SortConditions,
    IEnumerable<long> FolderIds,
    bool RecurseFolders,
    bool LatestOnly,
    string Bookmark,
    VaultSessionCredentials Session) : IRequest<VaultFileSearchResult>;


internal class SearchFilesHandler : IRequestHandler<SearchFilesCommand, VaultFileSearchResult>
{
    private const string Operation = "FindFilesBySearchConditions";

    private readonly ISoapRequestService _soapRequestService;

    public SearchFilesHandler(ISoapRequestService soapRequestService)
    {
        _soapRequestService = soapRequestService;
    }

    public async Task<VaultFileSearchResult> Handle(SearchFilesCommand command, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace ns)
        {
            content.AddElementsWithAttributes(ns, "SrchCond", command.SearchConditions);
            content.AddElementsWithAttributes(ns, "SrchSort", command.SortConditions);
            content.AddNestedElements(ns, "folderIds", "long", command.FolderIds.Select(x => x.ToString()));
            content.AddElement(ns, "recurseFolders", command.RecurseFolders);
            content.AddElement(ns, "latestOnly", command.LatestOnly);
            content.AddElement(ns, "bookmark", command.Bookmark);
        }

        XDocument responseBody = await _soapRequestService.SendAsync(Operation, command.Session, contentBuilder);
        var result = responseBody.ParseFileSearchResult();

        return result;
    }
}
