
using MediatR;

namespace FluentVault.RequestBuilders.Get.ClientShortcuts;
internal class GetClientShortcutsRequestBuilder : IRequestBuilder,
    IGetClientShortcutsRequestBuilder,
    IGetClientShortcutsClientSelector,
    IGetClientShortcutsEntitySelector,
    IGetClientShortcutsEndpoint
{
    private VaultClientType _clientType = VaultClientType.Thick;
    private VaultEntityClass _entityClass = VaultEntityClass.File;
    private IEnumerable<VaultMasterId> _masterIds = Enumerable.Empty<VaultMasterId>();
    private readonly IMediator _mediator;

    public GetClientShortcutsRequestBuilder(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IGetClientShortcutsEntitySelector WithClientType(VaultClientType clientType)
    {
        _clientType = clientType;
        return this;
    }

    public IGetClientShortcutsClientSelector WithEntityClass(VaultEntityClass entityClass)
    {
        _entityClass = entityClass;
        return this;
    }

    public IGetClientShortcutsEndpoint WithMasterId(VaultMasterId masterId) =>
        WithMasterIds(new[] { masterId });

    public IGetClientShortcutsEndpoint WithMasterIds(IEnumerable<VaultMasterId> masterIds)
    {
        _masterIds = masterIds;
        return this;
    }

    public async Task<IEnumerable<Uri>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        GetClientShortcutsQuery query = new(_masterIds, _entityClass, _clientType);
        IEnumerable<Uri> respone = await _mediator.Send(query, cancellationToken);
        return respone;
    }
}
