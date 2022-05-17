using FluentVault.Features;

using MediatR;

namespace FluentVault.RequestBuilders.Get.Properties;
internal class GetPropertiesRequestBuilder : IRequestBuilder, IGetPropertiesRequestBuilder, IGetPropertiesEntitySelector, IPropertySelector, IGetPropertiesEndpoint
{
    public GetPropertiesRequestBuilder(IMediator mediator)
    {
        _mediator = mediator;
    }

    private VaultEntityClass _entityClass = VaultEntityClass.File;
    private IEnumerable<VaultEntityId> _fileIds = Enumerable.Empty<VaultEntityId>();
    private IEnumerable<VaultPropertyDefinitionId> _propertyIds = Enumerable.Empty<VaultPropertyDefinitionId>();
    private readonly IMediator _mediator;

    public IGetPropertiesEntitySelector ForEntityClass(VaultEntityClass entityClass)
    {
        _entityClass = entityClass;
        return this;
    }

    public IPropertySelector WithId(VaultEntityId fileId)
        => WithIds(new VaultEntityId[] { fileId });

    public IPropertySelector WithIds(IEnumerable<VaultEntityId> fileIds)
    {
        _fileIds = fileIds;
        return this;
    }

    public IGetPropertiesEndpoint WithProperty(VaultPropertyDefinitionId propertyId)
        => WithProperties(new VaultPropertyDefinitionId[] { propertyId });

    public IGetPropertiesEndpoint WithProperties(IEnumerable<VaultPropertyDefinitionId> propertyIds)
    {
        _propertyIds = propertyIds;
        return this;
    }

    public async Task<IEnumerable<VaultPropertyInstance>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        GetPropertiesQuery query = new(_entityClass, _fileIds, _propertyIds);
        IEnumerable<VaultPropertyInstance> response = await _mediator.Send(query, cancellationToken);

        return response;
    }
}
