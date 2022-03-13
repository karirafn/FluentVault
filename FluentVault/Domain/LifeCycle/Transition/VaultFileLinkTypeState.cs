using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultFileLinkTypeState : SmartEnum<VaultFileLinkTypeState>
{
    public static readonly VaultFileLinkTypeState DesignDocs = new(nameof(DesignDocs), 1);
    public static readonly VaultFileLinkTypeState Primary = new(nameof(Primary), 2);
    public static readonly VaultFileLinkTypeState PrimarySub = new(nameof(PrimarySub), 3);
    public static readonly VaultFileLinkTypeState Secondary = new(nameof(Secondary), 4);
    public static readonly VaultFileLinkTypeState SecondarySub = new(nameof(SecondarySub), 5);
    public static readonly VaultFileLinkTypeState StandardComp = new(nameof(StandardComp), 6);
    public static readonly VaultFileLinkTypeState None = new(nameof(None), 7);

    private VaultFileLinkTypeState(string name, int value) : base(name, value) { }
}
