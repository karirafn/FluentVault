namespace FluentVault;

public interface ISearchFilesStringProperty
{
    public ISearchFilesAddSearchCondition InUserProperty(string property);
    public ISearchFilesAddSearchCondition InSystemProperty(SearchStringProperty property);
    public ISearchFilesAddSearchCondition InAllProperties { get; }
    public ISearchFilesAddSearchCondition InAllPropertiesAndContent { get; }
}
