namespace FluentVault;

public interface ISearchFilesRequestBuilder
{
    public ISearchFilesBooleanProperty ForValueEqualTo(bool value);
    public ISearchFilesBooleanProperty ForValueNotEqualTo(bool value);

    public ISearchFilesDateTimeProperty ForValueEqualTo(DateTime value);
    public ISearchFilesDateTimeProperty ForValueNotEqualTo(DateTime value);
    public ISearchFilesDateTimeProperty ForValueLessThan(DateTime value);
    public ISearchFilesDateTimeProperty ForValueLessThanOrEqualTo(DateTime value);
    public ISearchFilesDateTimeProperty ForValueGreaterThan(DateTime value);
    public ISearchFilesDateTimeProperty ForValueGreaterThanOrEqualTo(DateTime value);

    public ISearchFilesNumericProperty ForValueEqualTo(int value);
    public ISearchFilesNumericProperty ForValueNotEqualTo(int value);
    public ISearchFilesNumericProperty ForValueLessThan(int value);
    public ISearchFilesNumericProperty ForValueLessThanOrEqualTo(int value);
    public ISearchFilesNumericProperty ForValueGreaterThan(int value);
    public ISearchFilesNumericProperty ForValueGreaterThanOrEqualTo(int value);

    public ISearchFilesNumericProperty ForValueEqualTo(double value);
    public ISearchFilesNumericProperty ForValueNotEqualTo(double value);
    public ISearchFilesNumericProperty ForValueLessThan(double value);
    public ISearchFilesNumericProperty ForValueLessThanOrEqualTo(double value);
    public ISearchFilesNumericProperty ForValueGreaterThan(double value);
    public ISearchFilesNumericProperty ForValueGreaterThanOrEqualTo(double value);

    public ISearchFilesStringProperty ForValueEqualTo(string value);
    public ISearchFilesStringProperty ForValueContaining(string value);
    public ISearchFilesStringProperty ForValueNotContaining(string value);
}
