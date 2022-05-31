using System.Web;

using FluentVault;
using FluentVault.Features;

using MediatR;

using Microsoft.Extensions.Options;

internal record GetClientShortcutQuery(VaultMasterId MasterId, VaultEntityClass EntityClass, VaultClientType Type) : IRequest<Uri>;
internal class GetClientShortcutHandler : IRequestHandler<GetClientShortcutQuery, Uri>
{
    private const string ItemRevision = nameof(ItemRevision);

    private readonly VaultOptions _options;
    private readonly IMediator _mediator;

    public GetClientShortcutHandler(IMediator mediator, IOptions<VaultOptions> options)
    {
        _mediator = mediator;
        _options = options.Value;
    }

    public async Task<Uri> Handle(GetClientShortcutQuery query, CancellationToken cancellationToken)
    {
        string objectId = query.MasterId.ToString();

        string objectType = query.EntityClass.Name switch
        {
            "FILE" => nameof(VaultEntityClass.File),
            "ITEM" => ItemRevision,
            _ => throw new ArgumentException("Invalid entity")
        };

        if (query.Type.Equals(VaultClientType.Thick) && query.EntityClass.Equals(VaultEntityClass.File))
        {
            VaultFile file = await _mediator.Send(new GetLatestFileByMasterIdQuery(query.MasterId), cancellationToken);
            IEnumerable<VaultFolder> folders = await _mediator.Send(new GetFoldersByFileMasterIdsQuery(new [] { query.MasterId }), cancellationToken);
            objectId = HttpUtility.UrlEncode($"{folders.Single().Path}/{file.Filename}");
        }

        return query.Type.GetUri(_options.Server, _options.Database, objectId, objectType.ToLowerInvariant());
    }
}
