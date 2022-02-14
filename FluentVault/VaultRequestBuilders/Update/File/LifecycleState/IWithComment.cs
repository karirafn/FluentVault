namespace FluentVault;

public interface IWithComment
{
    public Task WithComment(string comment);
    public Task WithoutComment();
}
