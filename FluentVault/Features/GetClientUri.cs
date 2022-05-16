using System.Web;

using FluentVault;
using FluentVault.Domain.Client;
using FluentVault.Features;

using MediatR;

using Microsoft.Extensions.Options;

internal record GetClientUriQuery(VaultMasterId MasterId, VaultClientType Type) : IRequest<Uri>;
internal class GetClientUriHandler : IRequestHandler<GetClientUriQuery, Uri>
{
    private readonly VaultOptions _options;
    private readonly IMediator _mediator;

    public GetClientUriHandler(IMediator mediator, IOptions<VaultOptions> options)
    {
        _mediator = mediator;
        _options = options.Value;
    }

    public async Task<Uri> Handle(GetClientUriQuery query, CancellationToken cancellationToken)
    {
        string objectId;
        string objectType;

        if (await FileExists(query.MasterId, cancellationToken))
        {
            IEnumerable<VaultFolder> folders = await _mediator.Send(new GetFoldersByFileMasterIdsQuery(new[] { query.MasterId }), cancellationToken);
            objectId = HttpUtility.UrlEncode(folders.First().Path);
            objectType = "File";
        }
        else if (await ItemExists(query.MasterId, cancellationToken))
        {
            objectId = query.MasterId.Value.ToString();
            objectType = "ItemRevision";
        }
        else
        {
            throw new ArgumentException(@$"No entity found with master id ""{query.MasterId}""");
        }

        return query.Type.GetUri(_options.Server, _options.Database, objectId, objectType);
    }

    private async Task<bool> FileExists(VaultMasterId masterId, CancellationToken cancellationToken)
    {
        try { _ = await _mediator.Send(new GetLatestFileByMasterIdQuery(masterId), cancellationToken); }
        catch { return false; }

        return true;
    }

    private async Task<bool> ItemExists(VaultMasterId masterId, CancellationToken cancellationToken)
    {
        try { _ = await _mediator.Send(new GetLatestItemByItemMasterIdQuery(masterId), cancellationToken); }
        catch { return false; }

        return true;
    }
}
