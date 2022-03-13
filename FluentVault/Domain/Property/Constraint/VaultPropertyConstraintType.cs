using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultPropertyConstraintType : SmartEnum<VaultPropertyConstraintType>
{
    public static readonly VaultPropertyConstraintType Case = new(nameof(Case), 1);
    public static readonly VaultPropertyConstraintType EnforceListOfValues = new(nameof(EnforceListOfValues), 2);
    public static readonly VaultPropertyConstraintType MaximumDate = new(nameof(MaximumDate), 3);
    public static readonly VaultPropertyConstraintType MaximumLength = new(nameof(MaximumLength), 4);
    public static readonly VaultPropertyConstraintType MaximumValue = new(nameof(MaximumValue), 5);
    public static readonly VaultPropertyConstraintType MinimumDate = new(nameof(MinimumDate), 6);
    public static readonly VaultPropertyConstraintType MinimumLength = new(nameof(MinimumLength), 7);
    public static readonly VaultPropertyConstraintType MinimumValue = new(nameof(MinimumValue), 8);
    public static readonly VaultPropertyConstraintType RequiresValue = new(nameof(RequiresValue), 9);

    private VaultPropertyConstraintType(string name, int value) : base(name, value) { }
}
