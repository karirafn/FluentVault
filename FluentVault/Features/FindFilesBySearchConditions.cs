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
    private static readonly VaultRequest _request = new(
          operation: "FindFilesBySearchConditions",
          version: "v26",
          service: "DocumentService",
          command: "",
          @namespace: "Services/Document/1/7/2020");
    private readonly IVaultService _vaultService;

    public FindFilesBySearchConditionsHandler(IVaultService vaultService)
    {
        _vaultService = vaultService;
        Serializer = new(_request);
    }

    public FindFilesBySearchConditionsSerializer Serializer { get; }

    public async Task<VaultSearchFilesResponse> Handle(FindFilesBySearchConditionsQuery command, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace ns) => content
            .AddNestedElementsWithAttributes(ns, "conditions", "SrchCond", command.SearchConditions)
            .AddNestedElementsWithAttributes(ns, "sortConditions", "SrchSort", command.SortConditions)
            .AddNestedElements(ns, "folderIds", "long", command.FolderIds?.Select(id => id.ToString()))
            .AddElement(ns, "recurseFolders", command.RecurseFolders)
            .AddElement(ns, "latestOnly", command.LatestOnly)
            .AddElement(ns, "bookmark", command.Bookmark);

        XDocument response = await _vaultService.SendAsync(_request, canSignIn: true, contentBuilder, cancellationToken);
        VaultSearchFilesResponse result = Serializer.Deserialize(response);

        return result;
    }

    internal class FindFilesBySearchConditionsSerializer : XDocumentSerializer<VaultSearchFilesResponse>
    {
        public FindFilesBySearchConditionsSerializer(VaultRequest request)
            : base(request.Operation, new VaultSearchFilesResponseSerializer(request.Namespace)) { }
    }
}
