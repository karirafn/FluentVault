using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record GetUserInfosByIserIdsQuery(IEnumerable<VaultUserId> UserIds) : IRequest<IEnumerable<VaultUserInfo>>;
internal class GetUserInfosByUserIdsHandler : IRequestHandler<GetUserInfosByIserIdsQuery, IEnumerable<VaultUserInfo>>
{
    private static readonly VaultRequest _request = new(
          operation: "GetUserInfosByUserIds",
          version: "v26",
          service: "AdminService",
          command: "Connectivity.Explorer.Admin.SecurityCommand",
          @namespace: "Services/Admin/1/7/2020");
    private readonly IMediator _mediator;
    private readonly IVaultService _vaultService;

    public GetUserInfosByUserIdsHandler(IMediator mediator, IVaultService vaultService)
    {
        _mediator = mediator;
        _vaultService = vaultService;
    }

    public GetUserInfosByUserIdsSerializer Serializer { get; } = new(_request);

    public async Task<IEnumerable<VaultUserInfo>> Handle(GetUserInfosByIserIdsQuery query, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace ns) => content
            .AddNestedElements(ns, "userIdArray", "long", query.UserIds.Select(id => id.ToString()));

        XDocument response = await _mediator.SendAuthenticatedRequest(_request, _vaultService, contentBuilder, cancellationToken);
        IEnumerable<VaultUserInfo> result = Serializer.DeserializeMany(response);

        return result;
    }

    internal class GetUserInfosByUserIdsSerializer : XDocumentSerializer<VaultUserInfo>
    {
        public GetUserInfosByUserIdsSerializer(VaultRequest request)
            : base(request.Operation, new VaultUserInfoSerializer(request.Namespace)) { }
    }
}
