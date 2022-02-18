namespace FluentVault;

public interface ISearchFilesStringProperty
{
    public ISearchFilesAddSearchCondition InProperty(SearchStringProperty property);
    public ISearchFilesAddSearchCondition InAllProperties { get; }
    public ISearchFilesAddSearchCondition InAllPropertiesAndContent { get; }
}
