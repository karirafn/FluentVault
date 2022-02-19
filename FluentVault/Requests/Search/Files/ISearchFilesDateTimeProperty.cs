namespace FluentVault;

public interface ISearchFilesDateTimeProperty
{
    public ISearchFilesAddSearchCondition InUserProperty(string property);
    public ISearchFilesAddSearchCondition InSystemProperty(SearchDateTimeProperty property);
}
