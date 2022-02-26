namespace FluentVault;

public interface ISearchFilesBooleanProperty
{
    public ISearchFilesAddSearchCondition InUserProperty(string property);
    public ISearchFilesAddSearchCondition InSystemProperty(SearchBooleanProperties property);
}
