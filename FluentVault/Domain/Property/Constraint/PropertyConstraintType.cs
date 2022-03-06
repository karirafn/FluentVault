using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class PropertyConstraintType : SmartEnum<PropertyConstraintType>
{
    public static readonly PropertyConstraintType Case = new(nameof(Case), 1);
    public static readonly PropertyConstraintType EnforceListOfValues = new(nameof(EnforceListOfValues), 2);
    public static readonly PropertyConstraintType MaximumDate = new(nameof(MaximumDate), 3);
    public static readonly PropertyConstraintType MaximumLength = new(nameof(MaximumLength), 4);
    public static readonly PropertyConstraintType MaximumValue = new(nameof(MaximumValue), 5);
    public static readonly PropertyConstraintType MinimumDate = new(nameof(MinimumDate), 6);
    public static readonly PropertyConstraintType MinimumLength = new(nameof(MinimumLength), 7);
    public static readonly PropertyConstraintType MinimumValue = new(nameof(MinimumValue), 8);
    public static readonly PropertyConstraintType RequiresValue = new(nameof(RequiresValue), 9);

    private PropertyConstraintType(string name, int value) : base(name, value) { }
}
