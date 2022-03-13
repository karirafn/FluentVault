using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultClassification : SmartEnum<VaultClassification>
{
    public static readonly VaultClassification Standard = new(nameof(Standard), 1);
    public static readonly VaultClassification Custom = new(nameof(Custom), 2);
    public static readonly VaultClassification None = new(nameof(None), 3);

    private VaultClassification(string name, int value) : base(name, value) { }
}
