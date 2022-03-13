namespace FluentVault;

public interface ISearchFilesStringProperty
{
    public ISearchFilesAddSearchCondition InUserProperty(string property);
    public ISearchFilesAddSearchCondition InSystemProperty(StringSearchProperty property);
    public ISearchFilesAddSearchCondition InAllProperties { get; }
    public ISearchFilesAddSearchCondition InAllPropertiesAndContent { get; }
}
