
using FluentVault.Features;

using MediatR;

namespace FluentVault.RequestBuilders.Get.Latest.File.Associations;
internal class GetLatestFileAssociationsRequestBuilder : IRequestBuilder, IGetLatestFileAssociationsRequestBuilder, IGetLatestFileAssociationsEndpoint
{
    private readonly IMediator _mediator;
    private IEnumerable<VaultMasterId> _masterIds = Enumerable.Empty<VaultMasterId>();
    private VaultFileAssociationType _parentAssociationType = VaultFileAssociationType.None;
    private VaultFileAssociationType _childAssociationType = VaultFileAssociationType.None;
    private bool _recurseParents = false;
    private bool _recurseChildren = false;
    private bool _includeRelatedDocumentation = false;
    private bool _includeHidden = false;
    private bool _releasedBiased = false;

    public GetLatestFileAssociationsRequestBuilder(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IGetLatestFileAssociationsEndpoint ByMasterId(VaultMasterId masterId) =>
        ByMasterIds(new[] { masterId });

    public IGetLatestFileAssociationsEndpoint ByMasterIds(IEnumerable<VaultMasterId> masterIds)
    {
        _masterIds = masterIds;
        return this;
    }

    public IGetLatestFileAssociationsEndpoint RecurseParents
    {
        get
        {
            _recurseParents = true;
            return this;
        }
    }

    public IGetLatestFileAssociationsEndpoint RecurseChildren
    {
        get
        {
            _recurseChildren = true;
            return this;
        }
    }

    public IGetLatestFileAssociationsEndpoint IncludeRelatedDocumentation
    {
        get
        {
            _includeRelatedDocumentation = true;
            return this;
        }
    }

    public IGetLatestFileAssociationsEndpoint IncludeHidden
    {
        get
        {
            _includeHidden = true;
            return this;
        }
    }

    public IGetLatestFileAssociationsEndpoint ReleasedBiased
    {
        get
        {
            _releasedBiased = true;
            return this;
        }
    }

    public IGetLatestFileAssociationsEndpoint WithChildAssociation(VaultFileAssociationType type)
    {
        _childAssociationType = type;
        return this;
    }

    public IGetLatestFileAssociationsEndpoint WithParentAssociation(VaultFileAssociationType type)
    {
        _parentAssociationType = type;
        return this;
    }

    public async Task<IEnumerable<VaultFileAssociation>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        GetLatestFileAssociationsByMasterIdsQuery query = new(
            _masterIds,
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
