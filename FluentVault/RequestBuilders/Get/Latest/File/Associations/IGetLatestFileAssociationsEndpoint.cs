namespace FluentVault;

public interface IGetLatestFileAssociationsEndpoint
{
    public IGetLatestFileAssociationsRequestBuilder WithParentAssociation(VaultFileAssociationType type);
    public IGetLatestFileAssociationsRequestBuilder WithChildAssociation(VaultFileAssociationType type);
    public IGetLatestFileAssociationsRequestBuilder RecurseParents { get; }
    public IGetLatestFileAssociationsRequestBuilder RecurseChildren { get; }
    public IGetLatestFileAssociationsRequestBuilder IncludeRelatedDocumentation { get; }
    public IGetLatestFileAssociationsRequestBuilder IncludeHidden { get; }
    public IGetLatestFileAssociationsRequestBuilder ReleasedBiased { get; }
    public Task<IEnumerable<VaultFileAssociation>> ExecuteAsync(CancellationToken cancellationToken = default);
}
