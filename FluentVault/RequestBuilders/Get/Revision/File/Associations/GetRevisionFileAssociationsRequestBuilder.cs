using FluentVault.Features;

using MediatR;

namespace FluentVault.RequestBuilders.Get.Revision.File.Associations;
internal class GetRevisionFileAssociationsRequestBuilder : IRequestBuilder,
    IGetRevisionFileAssociationsRequestBuilder,
    IGetRevisionFileAssociationsEndPoint
{
    private readonly IMediator _mediator;

    private IEnumerable<VaultFileId> _ids = Enumerable.Empty<VaultFileId>();
    private VaultFileAssociationType _parentAssociationType = VaultFileAssociationType.None;
    private VaultFileAssociationType _childAssociationType = VaultFileAssociationType.None;
    private bool _recurseParents = false;
    private bool _recurseChildren = false;
    private bool _includeRelatedDocumentation = false;
    private bool _includeHidden = false;
    private bool _releasedBiased = false;

    public GetRevisionFileAssociationsRequestBuilder(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IGetRevisionFileAssociationsEndPoint ByFileIterationId(VaultFileId id) =>
        ByFileIterationIds(new VaultFileId[] { id });

    public IGetRevisionFileAssociationsEndPoint ByFileIterationIds(IEnumerable<VaultFileId> ids)
    {
        _ids = ids;
        return this;
    }

    public IGetRevisionFileAssociationsEndPoint RecurseParents
    {
        get
        {
            _recurseParents = true;
            return this;
        }
    }

    public IGetRevisionFileAssociationsEndPoint RecurseChildren
    {
        get
        {
            _recurseChildren = true;
            return this;
        }
    }

    public IGetRevisionFileAssociationsEndPoint IncludeRelatedDocumentation
    {
        get
        {
            _includeRelatedDocumentation = true;
            return this;
        }
    }

    public IGetRevisionFileAssociationsEndPoint IncludeHidden
    {
        get
        {
            _includeHidden = true;
            return this;
        }
    }

    public IGetRevisionFileAssociationsEndPoint ReleasedBiased
    {
        get
        {
            _releasedBiased = true;
            return this;
        }
    }

    public IGetRevisionFileAssociationsEndPoint WithChildAssociation(VaultFileAssociationType type)
    {
        _childAssociationType = type;
        return this;
    }

    public IGetRevisionFileAssociationsEndPoint WithParentAssociation(VaultFileAssociationType type)
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
