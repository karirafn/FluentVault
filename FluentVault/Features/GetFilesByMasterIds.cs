using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record GetFilesByMasterIdsQuery(IEnumerable<VaultMasterId> MasterIds) : IRequest<IEnumerable<VaultFile>>;
internal class GetFilesByMasterIdsHandler : IRequestHandler<GetFilesByMasterIdsQuery, IEnumerable<VaultFile>>
{
    private static readonly VaultRequest _request = new(
          operation: "GetFilesByMasterIds",
          version: "v26",
          service: "DocumentService",
          command: "",
          @namespace: "Services/Document/1/7/2020");
    private readonly IMediator _mediator;
    private readonly IVaultService _vaultService;

    public GetFilesByMasterIdsHandler(IMediator mediator, IVaultService vaultService)
    {
        _mediator = mediator;
        _vaultService = vaultService;
    }

    public GetFilesByMasterIdsSerializer Serializer { get; } = new(_request);

    public async Task<IEnumerable<VaultFile>> Handle(GetFilesByMasterIdsQuery query, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace @namespace) => content
            .AddNestedElements(@namespace, "fileMasterIds", "long", query.MasterIds);

        XDocument response = await _mediator.SendAuthenticatedRequest(_request, _vaultService, contentBuilder, cancellationToken);
        IEnumerable<VaultFile> result = Serializer.DeserializeMany(response);

        return result;
    }

    internal class GetFilesByMasterIdsSerializer : XDocumentSerializer<VaultFile>
    {
        public GetFilesByMasterIdsSerializer(VaultRequest request)
            : base(request.Operation, new VaultFileSerializer(request.Namespace)) { }
    }
}
