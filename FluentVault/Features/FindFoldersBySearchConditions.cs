﻿using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.Search.Folders;
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
    private static readonly VaultRequest _request = new(
          operation: "FindFoldersBySearchConditions",
          version: "v26",
          service: "DocumentService",
          command: "",
          @namespace: "Services/Document/1/7/2020");
    private readonly IMediator _mediator;
    private readonly IVaultService _vaultService;

    public FindFoldersBySearchConditionsHandler(IMediator mediator, IVaultService vaultService)
    {
        _mediator = mediator;
        _vaultService = vaultService;
    }

    public FindFoldersBySearchConditionsSerializer Serializer { get; } = new(_request);

    public async Task<VaultSearchFoldersResponse> Handle(FindFoldersBySearchConditionsQuery command, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace ns) => content
            .AddNestedElementsWithAttributes(ns, "conditions", "SrchCond", command.SearchConditions)
            .AddNestedElementsWithAttributes(ns, "sortConditions", "SrchSort", command.SortConditions)
            .AddNestedElements(ns, "folderIds", "long", command.FolderIds.Select(id => id.ToString()))
            .AddElement(ns, "recurseFolders", command.RecurseFolders)
            .AddElement(ns, "latestOnly", command.LatestOnly)
            .AddElement(ns, "bookmark", command.Bookmark);

        XDocument response = await _mediator.SendAuthenticatedRequest(_request, _vaultService, contentBuilder, cancellationToken);
        VaultSearchFoldersResponse result = Serializer.Deserialize(response);

        return result;
    }

    internal class FindFoldersBySearchConditionsSerializer : XDocumentSerializer<VaultSearchFoldersResponse>
    {
        public FindFoldersBySearchConditionsSerializer(VaultRequest request)
            : base(request.Operation, new VaultSearchFoldersResponseSerializer(request.Namespace)) { }
    }
}
