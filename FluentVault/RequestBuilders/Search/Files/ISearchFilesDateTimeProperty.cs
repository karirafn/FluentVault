namespace FluentVault;

public interface ISearchFilesDateTimeProperty
{
    public ISearchFilesAddSearchCondition InUserProperty(string property);
    public ISearchFilesAddSearchCondition InSystemProperty(DateTimeSearchProperty property);
}
