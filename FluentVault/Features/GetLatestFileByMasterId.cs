using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;
internal record GetLatestFileByMasterIdQuery(VaultMasterId MasterId) : IRequest<VaultFile>;
internal class GetLatestFileByMasterIdHandler : IRequestHandler<GetLatestFileByMasterIdQuery, VaultFile>
{
    private const string Operation = "GetLatestFileByMasterId";

    private readonly IVaultService _vaultService;

    public GetLatestFileByMasterIdHandler(IVaultService vaultService)
    {
        _vaultService = vaultService;
    }

    public async Task<VaultFile> Handle(GetLatestFileByMasterIdQuery query, CancellationToken cancellationToken)
    {
        void contentBuilder(XElement content, XNamespace ns)
        {
            content.AddElement(ns, "fileMasterId", query.MasterId.Value);
        };

        XDocument document = await _vaultService.SendAsync(Operation, canSignIn: true, contentBuilder, cancellationToken);
        VaultFile file = VaultFile.ParseSingle(document);

        return file;
    }
}
