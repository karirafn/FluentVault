using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record GetLatestFileByMasterIdQuery(VaultMasterId MasterId) : IRequest<VaultFile>;
internal class GetLatestFileByMasterIdHandler : IRequestHandler<GetLatestFileByMasterIdQuery, VaultFile>
{
    private static readonly VaultRequest _request = new(
          operation: "GetLatestFileByMasterId",
          version: "v26",
          service: "DocumentService",
          command: "Connectivity.Explorer.Document.FileSendUrlCommand",
          @namespace: "Services/Document/1/7/2020");
    private readonly IVaultService _vaultService;

    public GetLatestFileByMasterIdHandler(IVaultService vaultService)
    {
        _vaultService = vaultService;
        Serializer = new(_request);
    }

    public GetLatestFileByMasterIdSerializer Serializer { get; }

    public async Task<VaultFile> Handle(GetLatestFileByMasterIdQuery query, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace ns) => content
            .AddElement(ns, "fileMasterId", query.MasterId.Value);

        XDocument document = await _vaultService.SendAuthenticatedAsync(_request, contentBuilder, cancellationToken);
        VaultFile file = Serializer.Deserialize(document);

        return file;
    }

    internal class GetLatestFileByMasterIdSerializer : XDocumentSerializer<VaultFile>
    {
        private readonly VaultRequest _request;

        public GetLatestFileByMasterIdSerializer(VaultRequest request)
            : base(request.Operation, new VaultFileSerializer($"{request.Operation}Result", request.Namespace))
        {
            _request = request;
        }

        public override XDocument Serialize(VaultFile file)
            => new XDocument().AddResponse(_request.Operation, _request.Namespace, ElementSerializer.Serialize(file), null);
    }
}
