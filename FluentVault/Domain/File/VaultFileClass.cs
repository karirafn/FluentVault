
using Ardalis.SmartEnum;

namespace FluentVault;
public sealed class VaultFileClass : SmartEnum<VaultFileClass>
{
    public static readonly VaultFileClass ConfigurationFactory = new(nameof(ConfigurationFactory), 1);
    public static readonly VaultFileClass ConfigurationMember = new(nameof(ConfigurationMember), 2);
    public static readonly VaultFileClass DesignDocument = new(nameof(DesignDocument), 3);
    public static readonly VaultFileClass DesignPresentation = new(nameof(DesignPresentation), 4);
    public static readonly VaultFileClass DesignRepresentation = new(nameof(DesignRepresentation), 5);
    public static readonly VaultFileClass DesignSubstitute = new(nameof(DesignSubstitute), 6);
    public static readonly VaultFileClass DesignVisualization = new(nameof(DesignVisualization), 7);
    public static readonly VaultFileClass ElectricalProject = new(nameof(ElectricalProject), 8);
    public static readonly VaultFileClass None = new(nameof(None), 9);

    private VaultFileClass(string name, int value) : base(name, value) { }
}
