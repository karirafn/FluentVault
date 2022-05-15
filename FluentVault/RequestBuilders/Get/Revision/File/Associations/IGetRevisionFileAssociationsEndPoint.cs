namespace FluentVault;

public interface IGetRevisionFileAssociationsEndPoint
{
    public IGetRevisionFileAssociationsEndPoint WithParentAssociation(VaultFileAssociationType type);
    public IGetRevisionFileAssociationsEndPoint WithChildAssociation(VaultFileAssociationType type);
    public IGetRevisionFileAssociationsEndPoint RecurseParents { get; }
    public IGetRevisionFileAssociationsEndPoint RecurseChildren { get; }
    public IGetRevisionFileAssociationsEndPoint IncludeRelatedDocumentation { get; }
    public IGetRevisionFileAssociationsEndPoint IncludeHidden { get; }
    public IGetRevisionFileAssociationsEndPoint ReleasedBiased { get; }
    public Task<IEnumerable<VaultFileAssociation>> ExecuteAsync(CancellationToken cancellationToken = default);
}
