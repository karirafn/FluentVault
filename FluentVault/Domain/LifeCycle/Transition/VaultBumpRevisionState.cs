using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultBumpRevisionState : SmartEnum<VaultBumpRevisionState>
{
    public static readonly VaultBumpRevisionState BumpProperty = new(nameof(BumpProperty), 1);
    public static readonly VaultBumpRevisionState BumpSecondary = new(nameof(BumpSecondary), 2);
    public static readonly VaultBumpRevisionState BumpTertiary = new(nameof(BumpTertiary), 3);
    public static readonly VaultBumpRevisionState None = new(nameof(None), 4);

    private VaultBumpRevisionState(string name, int value) : base(name, value) { }
}
