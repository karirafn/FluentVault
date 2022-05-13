
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
    private bool _releasedBiased = false;

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

    public IGetFileAssociationsEndPoint RecurseParents
    {
        get
        {
            _recurseParents = true;
            return this;
        }
    }

    public IGetFileAssociationsEndPoint RecurseChildren
    {
        get
        {
            _recurseChildren = true;
            return this;
        }
    }

    public IGetFileAssociationsEndPoint IncludeRelatedDocumentation
    {
        get
        {
            _includeRelatedDocumentation = true;
            return this;
        }
    }

    public IGetFileAssociationsEndPoint IncludeHidden
    {
        get
        {
            _includeHidden = true;
            return this;
        }
    }

    public IGetFileAssociationsEndPoint ReleasedBiased
    {
        get
        {
            _releasedBiased = true;
            return this;
        }
    }

    public IGetFileAssociationsEndPoint WithChildAssociation(VaultFileAssociationType type)
    {
        _childAssociationType = type;
        return this;
    }

    public IGetFileAssociationsEndPoint WithParentAssociation(VaultFileAssociationType type)
    {
        _parentAssociationType = type;
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
            _releasedBiased);
        IEnumerable<VaultFileAssociation> response = await _mediator.Send(query, cancellationToken);

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
    public IGetFileAssociationsEndPoint WithParentAssociation(VaultFileAssociationType type);
    public IGetFileAssociationsEndPoint WithChildAssociation(VaultFileAssociationType type);
    public IGetFileAssociationsEndPoint RecurseParents { get; }
    public IGetFileAssociationsEndPoint RecurseChildren { get; }
    public IGetFileAssociationsEndPoint IncludeRelatedDocumentation { get; }
    public IGetFileAssociationsEndPoint IncludeHidden { get; }
    public IGetFileAssociationsEndPoint ReleasedBiased { get; }
    public Task<IEnumerable<VaultFileAssociation>> ExecuteAsync(CancellationToken cancellationToken = default);
}
