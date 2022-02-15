namespace FluentVault;

public interface IWithComment
{
    public Task<VaultFile> WithComment(string comment);
    public Task<VaultFile> WithoutComment();
}
