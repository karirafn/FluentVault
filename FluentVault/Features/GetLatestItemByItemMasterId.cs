using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.Item;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record GetLatestItemByItemMasterIdQuery(VaultMasterId MasterId) : IRequest<VaultItem>;
internal class GetLatestItemByItemMasterIdHandler : IRequestHandler<GetLatestItemByItemMasterIdQuery, VaultItem>
{
    private const string Operation = "GetLatestItemByItemMasterId";

    private readonly IVaultService _vaultService;

    public GetLatestItemByItemMasterIdHandler(IVaultService vaultService)
    {
        _vaultService = vaultService;
    }

    public async Task<VaultItem> Handle(GetLatestItemByItemMasterIdQuery query, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace @namespace)
            => content.AddElement(@namespace, "itemMasterId", query.MasterId);

        XDocument document = await _vaultService.SendAsync(Operation, canSignIn: true, contentBuilder, cancellationToken);
        VaultItem item = new GetLatestItemByItemMasterIdSerializer().Deserialize(document);

        return item;
    }
}

internal class GetLatestItemByItemMasterIdSerializer : XDocumentSerializer<VaultItem>
{
    private const string GetLatestItemByItemMasterId = nameof(GetLatestItemByItemMasterId);
    private static readonly VaultRequest _request = new VaultRequestData().Get(GetLatestItemByItemMasterId);

    public GetLatestItemByItemMasterIdSerializer() : base(_request.Operation, new VaultItemSerializer(_request.Namespace)) { }
}
