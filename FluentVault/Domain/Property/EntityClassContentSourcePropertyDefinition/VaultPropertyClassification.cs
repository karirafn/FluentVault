using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultPropertyClassification : SmartEnum<VaultPropertyClassification>
{
    public static readonly VaultPropertyClassification Standard = new(nameof(Standard), 1);
    public static readonly VaultPropertyClassification Custom = new(nameof(Custom), 2);
    public static readonly VaultPropertyClassification None = new(nameof(None), 3);

    private VaultPropertyClassification(string name, int value) : base(name, value) { }
}
