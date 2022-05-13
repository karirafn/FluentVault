
using Ardalis.SmartEnum;

namespace FluentVault;
public sealed class VaultFileAssociationType : SmartEnum<VaultFileAssociationType>
{
    public static readonly VaultFileAssociationType Attachment = new(nameof(Attachment), 1);
    public static readonly VaultFileAssociationType Dependency = new(nameof(Dependency), 2);
    public static readonly VaultFileAssociationType All = new(nameof(All), 3);
    public static readonly VaultFileAssociationType None = new(nameof(None), 4);

    private VaultFileAssociationType(string name, int value) : base(name, value) { }
}
