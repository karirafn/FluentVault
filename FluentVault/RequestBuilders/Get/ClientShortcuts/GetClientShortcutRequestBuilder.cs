
using MediatR;

namespace FluentVault.RequestBuilders.Get.ClientShortcuts;
internal class GetClientShortcutRequestBuilder : IRequestBuilder,
    IGetClientShortcutRequestBuilder,
    IGetClientShortcutClientSelector,
    IGetClientShortcutEntitySelector,
    IGetClientShortcutEndpoint
{
    private VaultClientType _clientType = VaultClientType.Thick;
    private VaultEntityClass _entityClass = VaultEntityClass.File;
    private VaultMasterId _masterId = new(0);
    private readonly IMediator _mediator;

    public GetClientShortcutRequestBuilder(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IGetClientShortcutEntitySelector WithClientType(VaultClientType clientType)
    {
        _clientType = clientType;
        return this;
    }

    public IGetClientShortcutClientSelector WithEntityClass(VaultEntityClass entityClass)
    {
        _entityClass = entityClass;
        return this;
    }

    public IGetClientShortcutEndpoint WithMasterId(VaultMasterId masterId)
    {
        _masterId = masterId;
        return this;
    }

    public async Task<Uri> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        GetClientShortcutQuery query = new(_masterId, _entityClass, _clientType);
        Uri respone = await _mediator.Send(query, cancellationToken);
        return respone;
    }
}
