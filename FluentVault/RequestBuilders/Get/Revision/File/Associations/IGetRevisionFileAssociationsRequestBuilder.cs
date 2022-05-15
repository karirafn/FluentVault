namespace FluentVault;

public interface IGetRevisionFileAssociationsRequestBuilder
{
    public IGetRevisionFileAssociationsEndPoint ByFileIterationId(VaultFileId id);
    public IGetRevisionFileAssociationsEndPoint ByFileIterationIds(IEnumerable<VaultFileId> ids);
}
