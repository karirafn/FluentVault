using System.Text;
using System.Xml.Linq;

using FluentVault.Common.Extensions;
using FluentVault.Domain.Search;
using FluentVault.Domain.SOAP;

using MediatR;

namespace FluentVault.Features;

internal class SearchFilesHandler : IRequestHandler<SearchFilesCommand, VaultFileSearchResult>
{
    private const string RequestName = "FindFilesBySearchConditions";

    private readonly ISoapRequestService _soapRequestService;

    public SearchFilesHandler(ISoapRequestService soapRequestService)
    {
        _soapRequestService = soapRequestService;
    }

    public async Task<VaultFileSearchResult> Handle(SearchFilesCommand command, CancellationToken cancellationToken)
    {
        string requestBody = GetRequestBody(command);
        XDocument document = await _soapRequestService.SendAsync(RequestName, requestBody);
        var result = document.ParseFileSearchResult();

        return result;
    }

    private string GetRequestBody(SearchFilesCommand command)
        => new StringBuilder()
            .AppendRequestBodyOpening(command.Session)
            .AppendElementWithAttribute(RequestName, "xmlns", _soapRequestService.GetNamespace(RequestName))
            .AppendNestedElementsWithAttributes("conditions", "SrchCond", command.SearchConditions)
            .AppendNestedElementsWithAttributes("sortConditions", "SrchSort", command.SortConditions)
            .AppendElements("folderIds", command.FolderIds)
            .AppendElement("recurseFolders", command.RecurseFolders)
            .AppendElement("latestOnly", command.LatestOnly)
            .AppendElement("bookmark", command.Bookmark)
            .AppendElementClosing(RequestName)
            .AppendRequestBodyClosing()
            .ToString();
}
