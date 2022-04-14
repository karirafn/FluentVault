using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record GetFoldersByFileMasterIdsQuery(IEnumerable<VaultMasterId> MasterIds) : IRequest<IEnumerable<VaultFolder>>;
internal class GetFoldersByFileMasterIdsHandler : IRequestHandler<GetFoldersByFileMasterIdsQuery, IEnumerable<VaultFolder>>
{
    private const string Operation = "GetFoldersByFileMasterIds";

    private readonly IVaultService _vaultService;

    public GetFoldersByFileMasterIdsHandler(IVaultService vaultService)
    {
        _vaultService = vaultService;
    }

    public async Task<IEnumerable<VaultFolder>> Handle(GetFoldersByFileMasterIdsQuery query, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace @namespace)
            => content.AddNestedElements(@namespace, "fileMasterIds", "long", query.MasterIds);

        XDocument document = await _vaultService.SendAsync(Operation, canSignIn: true, contentBuilder, cancellationToken);
        IEnumerable<VaultFolder> folders = new GetFoldersByFileMasterIdsSerializer().DeserializeMany(document);

        return folders;
    }
}

internal class GetFoldersByFileMasterIdsSerializer : XDocumentSerializer<VaultFolder>
{
    private const string GetFoldersByFileMasterIds = nameof(GetFoldersByFileMasterIds);
    private static readonly VaultRequest _request = new VaultRequestData().Get(GetFoldersByFileMasterIds);

    public GetFoldersByFileMasterIdsSerializer() : base(_request.Operation, new VaultFolderSerializer(_request.Namespace)) { }
}
