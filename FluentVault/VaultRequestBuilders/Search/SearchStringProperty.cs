namespace FluentVault;

public class SearchStringProperty
{
    public static readonly SearchStringProperty Filename = new(9);
    public static readonly SearchStringProperty LifecycleState = new(49);

    private SearchStringProperty(int id) => Id = id;

    public int Id { get; init; }

    public override string ToString() => Id.ToString();
}
