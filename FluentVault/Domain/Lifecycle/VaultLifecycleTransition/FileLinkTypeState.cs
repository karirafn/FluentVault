using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class FileLinkTypeState : SmartEnum<FileLinkTypeState>
{
    public static readonly FileLinkTypeState DesignDocs = new(nameof(DesignDocs), 1);
    public static readonly FileLinkTypeState Primary = new(nameof(Primary), 2);
    public static readonly FileLinkTypeState PrimarySub = new(nameof(PrimarySub), 3);
    public static readonly FileLinkTypeState Secondary = new(nameof(Secondary), 4);
    public static readonly FileLinkTypeState SecondarySub = new(nameof(SecondarySub), 5);
    public static readonly FileLinkTypeState StandardComp = new(nameof(StandardComp), 6);
    public static readonly FileLinkTypeState None = new(nameof(None), 7);

    private FileLinkTypeState(string name, int value) : base(name, value) { }
}
