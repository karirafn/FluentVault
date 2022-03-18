using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;

internal record GetUserInfosByIserIdsQuery(IEnumerable<VaultUserId> UserIds) : IRequest<IEnumerable<VaultUserInfo>>;

internal class GetUserInfosByUserIdsHandler : IRequestHandler<GetUserInfosByIserIdsQuery, IEnumerable<VaultUserInfo>>
{
    private const string Operation = "GetUserInfosByUserIds";

    private readonly IVaultService _vaultRequestService;

    public GetUserInfosByUserIdsHandler(IVaultService vaultRequestService)
        => _vaultRequestService = vaultRequestService;

    public async Task<IEnumerable<VaultUserInfo>> Handle(GetUserInfosByIserIdsQuery query, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace ns)
            => content.AddNestedElements(ns, "userIdArray", "long", query.UserIds.Select(id => id.ToString()));

        XDocument response = await _vaultRequestService.SendAsync(Operation, canSignIn: true, contentBuilder, cancellationToken);
        IEnumerable<VaultUserInfo> result = VaultUserInfo.ParseAll(response);

        return result;
    }
}
