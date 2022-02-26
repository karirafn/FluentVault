namespace FluentVault;

public interface ISearchFilesNumericProperty
{
    public ISearchFilesAddSearchCondition InUserProperty(string property);
    public ISearchFilesAddSearchCondition InSystemProperty(SearchNumericProperty property);
}
