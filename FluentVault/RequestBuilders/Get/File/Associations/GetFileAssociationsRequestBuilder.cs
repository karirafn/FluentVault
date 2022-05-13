
using FluentVault.Features;

using MediatR;

namespace FluentVault.RequestBuilders.Get.File.Associations;
internal class GetFileAssociationsRequestBuilder : IRequestBuilder,
    IGetFileAssociationsRequestBuilder,
    IGetFileAssociationsEndPoint
{
    private readonly IMediator _mediator;

    private IEnumerable<VaultFileIterationId> _ids = Enumerable.Empty<VaultFileIterationId>();
    private VaultFileAssociationType _parentAssociationType = VaultFileAssociationType.None;
    private VaultFileAssociationType _childAssociationType = VaultFileAssociationType.None;
    private bool _recurseParents = false;
    private bool _recurseChildren = false;
    private bool _includeRelatedDocumentation = false;
    private bool _includeHidden = false;
    private bool _releaseBiased = false;

    public GetFileAssociationsRequestBuilder(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IGetFileAssociationsEndPoint ByFileIterationId(VaultFileIterationId id) =>
        ByFileIterationIds(new VaultFileIterationId[] { id });

    public IGetFileAssociationsEndPoint ByFileIterationIds(IEnumerable<VaultFileIterationId> ids)
    {
        _ids = ids;
        return this;
    }

    public async Task<IEnumerable<VaultFileAssociation>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        GetRevisionFileAssociationsByIdsQuery query = new(
            _ids,
            _parentAssociationType,
            _recurseParents,
            _childAssociationType,
            _recurseChildren,
            _includeRelatedDocumentation,
            _includeHidden,
            _releaseBiased);
        IEnumerable<VaultFileAssociation> response = await _mediator.Send(query);

        return response;
    }
}

public interface IGetFileAssociationsRequestBuilder
{
    public IGetFileAssociationsEndPoint ByFileIterationId(VaultFileIterationId id);
    public IGetFileAssociationsEndPoint ByFileIterationIds(IEnumerable<VaultFileIterationId> ids);
}

public interface IGetFileAssociationsEndPoint
{
    public Task<IEnumerable<VaultFileAssociation>> ExecuteAsync(CancellationToken cancellationToken = default);
}
