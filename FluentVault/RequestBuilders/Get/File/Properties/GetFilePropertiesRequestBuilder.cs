
using FluentVault.Features;

using MediatR;

namespace FluentVault.RequestBuilders.Get.File.Properties;
internal class GetFilePropertiesRequestBuilder : IRequestBuilder, IGetFilePropertiesRequestBuilder, IPropertySelector, IGetFilePropertiesEndpoint
{
    public GetFilePropertiesRequestBuilder(IMediator mediator)
    {
        _mediator = mediator;
    }

    private IEnumerable<VaultEntityId> _fileIds = Enumerable.Empty<VaultEntityId>();
    private IEnumerable<VaultPropertyDefinitionId> _propertyIds = Enumerable.Empty<VaultPropertyDefinitionId>();
    private readonly IMediator _mediator;

    public IGetFilePropertiesEndpoint AndProperty(VaultPropertyDefinitionId propertyId)
        => AndProperties(new VaultPropertyDefinitionId[] { propertyId });

    public IGetFilePropertiesEndpoint AndProperties(IEnumerable<VaultPropertyDefinitionId> propertyIds)
    {
        _propertyIds = propertyIds;
        return this;
    }

    public IPropertySelector ByFileId(VaultEntityId fileId)
        => ByFileIds(new VaultEntityId[] { fileId });

    public IPropertySelector ByFileIds(IEnumerable<VaultEntityId> fileIds)
    {
        _fileIds = fileIds;
        return this;
    }

    public async Task<IEnumerable<VaultPropertyInstance>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        GetPropertiesQuery query = new(VaultEntityClass.File, _fileIds, _propertyIds);
        IEnumerable<VaultPropertyInstance> response = await _mediator.Send(query, cancellationToken);

        return response;
    }
}
