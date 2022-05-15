namespace FluentVault;

public interface IGetLatestFileAssociationsEndpoint
{
    public IGetLatestFileAssociationsEndpoint WithParentAssociation(VaultFileAssociationType type);
    public IGetLatestFileAssociationsEndpoint WithChildAssociation(VaultFileAssociationType type);
    public IGetLatestFileAssociationsEndpoint RecurseParents { get; }
    public IGetLatestFileAssociationsEndpoint RecurseChildren { get; }
    public IGetLatestFileAssociationsEndpoint IncludeRelatedDocumentation { get; }
    public IGetLatestFileAssociationsEndpoint IncludeHidden { get; }
    public IGetLatestFileAssociationsEndpoint ReleasedBiased { get; }
    public Task<IEnumerable<VaultFileAssociation>> ExecuteAsync(CancellationToken cancellationToken = default);
}
