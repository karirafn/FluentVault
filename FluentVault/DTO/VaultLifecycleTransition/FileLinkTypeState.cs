namespace FluentVault;

public class FileLinkTypeState : BaseType
{
    public static readonly FileLinkTypeState DesignDocs = new(nameof(DesignDocs));
    public static readonly FileLinkTypeState Primary = new(nameof(Primary));
    public static readonly FileLinkTypeState PrimarySub = new(nameof(PrimarySub));
    public static readonly FileLinkTypeState Secondary = new(nameof(Secondary));
    public static readonly FileLinkTypeState SecondarySub = new(nameof(SecondarySub));
    public static readonly FileLinkTypeState StandardComp = new(nameof(StandardComp));
    public static readonly FileLinkTypeState None = new(nameof(None));

    private FileLinkTypeState(string value) : base(value) { }

    public static FileLinkTypeState Parse(string value)
        => Parse(value, new[] { DesignDocs, Primary, PrimarySub, Secondary, SecondarySub, StandardComp, None });
}
