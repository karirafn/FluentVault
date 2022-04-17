using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.Item;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record GetLatestItemByItemMasterIdQuery(VaultMasterId MasterId) : IRequest<VaultItem>;
internal class GetLatestItemByItemMasterIdHandler : IRequestHandler<GetLatestItemByItemMasterIdQuery, VaultItem>
{
    private static readonly VaultRequest _request = new(
          operation: "GetLatestItemByItemMasterId",
          version: "v26",
          service: "ItemService",
          command: "",
          @namespace: "Services/Document/1/7/2020");
    private readonly IVaultService _vaultService;

    public GetLatestItemByItemMasterIdHandler(IVaultService vaultService)
    {
        _vaultService = vaultService;
        Serializer = new(_request);
    }

    public GetLatestItemByItemMasterIdSerializer Serializer { get; }

    public async Task<VaultItem> Handle(GetLatestItemByItemMasterIdQuery query, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace @namespace) => content
            .AddElement(@namespace, "itemMasterId", query.MasterId);

        XDocument document = await _vaultService.SendAsync(_request, canSignIn: true, contentBuilder, cancellationToken);
        VaultItem item = Serializer.Deserialize(document);

        return item;
    }

    internal class GetLatestItemByItemMasterIdSerializer : XDocumentSerializer<VaultItem>
    {
        private readonly VaultRequest _request;

        public GetLatestItemByItemMasterIdSerializer(VaultRequest request)
            : base(request.Operation, new VaultItemSerializer($"{request.Operation}Result", request.Namespace))
        {
            _request = request;
        }

        public override XDocument Serialize(VaultItem item)
            => new XDocument().AddResponse(_request.Operation, _request.Namespace, ElementSerializer.Serialize(item), null);
    }
}
