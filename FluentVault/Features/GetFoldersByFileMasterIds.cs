using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record GetFoldersByFileMasterIdsQuery(IEnumerable<VaultMasterId> MasterIds) : IRequest<IEnumerable<VaultFolder>>;
internal class GetFoldersByFileMasterIdsHandler : IRequestHandler<GetFoldersByFileMasterIdsQuery, IEnumerable<VaultFolder>>
{
    private static readonly VaultRequest _request = new(
          operation: "GetFoldersByFileMasterIds",
          version: "v26",
          service: "DocumentService",
          command: "",
          @namespace: "Services/Document/1/7/2020");
    private readonly IVaultService _vaultService;

    public GetFoldersByFileMasterIdsHandler(IVaultService vaultService)
    {
        _vaultService = vaultService;
        Serializer = new(_request);
    }

    public GetFoldersByFileMasterIdsSerializer Serializer { get; }

    public async Task<IEnumerable<VaultFolder>> Handle(GetFoldersByFileMasterIdsQuery query, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace @namespace) => content
            .AddNestedElements(@namespace, "fileMasterIds", "long", query.MasterIds);

        XDocument document = await _vaultService.SendAuthenticatedAsync(_request, contentBuilder, cancellationToken);
        IEnumerable<VaultFolder> folders = Serializer.DeserializeMany(document);

        return folders;
    }

    internal class GetFoldersByFileMasterIdsSerializer : XDocumentSerializer<VaultFolder>
    {
        public GetFoldersByFileMasterIdsSerializer(VaultRequest request)
            : base(request.Operation, new VaultFolderSerializer(request.Namespace)) { }
    }
}
