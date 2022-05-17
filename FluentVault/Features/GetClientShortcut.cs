using System.Web;

using FluentVault;
using FluentVault.Features;

using MediatR;

using Microsoft.Extensions.Options;

internal record GetClientShortcutsQuery(IEnumerable<VaultMasterId> MasterIds, VaultEntityClass EntityClass, VaultClientType Type) : IRequest<IEnumerable<Uri>>;
internal class GetClientShortcutsHandler : IRequestHandler<GetClientShortcutsQuery, IEnumerable<Uri>>
{
    private const string ItemRevision = nameof(ItemRevision);

    private readonly VaultOptions _options;
    private readonly IMediator _mediator;

    public GetClientShortcutsHandler(IMediator mediator, IOptions<VaultOptions> options)
    {
        _mediator = mediator;
        _options = options.Value;
    }

    public async Task<IEnumerable<Uri>> Handle(GetClientShortcutsQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<string> objectIds = Enumerable.Empty<string>();
        string objectType;

        if (query.EntityClass.Equals(VaultEntityClass.File))
        {
            IEnumerable<VaultFolder> folders = await _mediator.Send(new GetFoldersByFileMasterIdsQuery(query.MasterIds), cancellationToken);
            objectIds = folders.Select(folder => HttpUtility.UrlEncode(folder.Path));
            objectType = nameof(VaultEntityClass.File);
        }
        else if (query.EntityClass.Equals(VaultEntityClass.Item))
        {
            objectIds = query.MasterIds.Select(id => id.Value.ToString());
            objectType = ItemRevision;
        }
        else
        {
            throw new ArgumentException("Invalid entity");
        }

        return objectIds.Select(objectId => query.Type.GetUri(_options.Server, _options.Database, objectId, objectType));
    }
}
