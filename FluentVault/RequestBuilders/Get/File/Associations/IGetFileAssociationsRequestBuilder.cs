namespace FluentVault;

public interface IGetFileAssociationsRequestBuilder
{
    public IGetFileAssociationsEndPoint ByFileIterationId(VaultFileId id);
    public IGetFileAssociationsEndPoint ByFileIterationIds(IEnumerable<VaultFileId> ids);
}
