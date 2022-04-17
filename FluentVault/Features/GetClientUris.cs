using System.Web;

using MediatR;

using Microsoft.Extensions.Options;

namespace FluentVault.Features;
internal record GetClientUrisQuery(VaultMasterId MasterId) : IRequest<(Uri VaultClient, Uri ThinClient)>;
internal class GetClientUrisHandler : IRequestHandler<GetClientUrisQuery, (Uri VaultClient, Uri ThinClient)>
{
    private readonly VaultOptions _options;
    private readonly IMediator _mediator;

    public GetClientUrisHandler(IMediator mediator, IOptions<VaultOptions> options)
    {
        _mediator = mediator;
        _options = options.Value;
    }

    public async Task<(Uri VaultClient, Uri ThinClient)> Handle(GetClientUrisQuery query, CancellationToken cancellationToken)
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

        Uri vaultClient = GetVaultClientUri(objectId, objectType);
        Uri thinClient = GetThinClientUri(objectType, query.MasterId);

        return (vaultClient, thinClient);
    }

    private Uri GetVaultClientUri(string objectId, string objectType)
        => new($"http://{_options.Server}/AutodeskDM/Services/EntityDataCommandRequest.aspx?Vault={_options.Database}&ObjectId={objectId}&ObjectType={objectType}&Command=Select");

    private Uri GetThinClientUri(string objectType, VaultMasterId masterId)
        => new($"http://{_options.Server}/AutodeskTC/{_options.Database}/Explore/{objectType}/{masterId}");

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
