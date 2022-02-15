namespace FluentVault;

public interface ISearchFilesRequestBuilder
{
    public ISearchFilesStringProperty ForValueContaining(string value);
    public ISearchFilesStringProperty ForValueNotContaining(string value);
}
