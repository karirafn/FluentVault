namespace FluentVault;

public interface ISearchFilesRequest
{
    public ISearchFilesStringProperty ForValueEqualTo(string value);
    public ISearchFilesStringProperty ForValueContaining(string value);
    public ISearchFilesStringProperty ForValueNotContaining(string value);

    public ISearchFilesDateTimeProperty ForValueEqualTo(DateTime value);
    public ISearchFilesDateTimeProperty ForValueNotEqualTo(DateTime value);
    public ISearchFilesDateTimeProperty ForValueLessThan(DateTime value);
    public ISearchFilesDateTimeProperty ForValueLessThanOrEqualTo(DateTime value);
    public ISearchFilesDateTimeProperty ForValueGreaterThan(DateTime value);
    public ISearchFilesDateTimeProperty ForValueGreaterThanOrEqualTo(DateTime value);
}
