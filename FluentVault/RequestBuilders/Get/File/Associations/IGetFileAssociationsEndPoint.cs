namespace FluentVault;

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
