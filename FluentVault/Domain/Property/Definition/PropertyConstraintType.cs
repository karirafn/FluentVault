using FluentVault.Domain.Common;

namespace FluentVault;

public class PropertyConstraintType : BaseType
{
    public static readonly PropertyConstraintType Case = new(nameof(Case));
    public static readonly PropertyConstraintType EnforceListOfValues = new(nameof(EnforceListOfValues));
    public static readonly PropertyConstraintType MaximumDate = new(nameof(MaximumDate));
    public static readonly PropertyConstraintType MaximumLength = new(nameof(MaximumLength));
    public static readonly PropertyConstraintType MaximumValue = new(nameof(MaximumValue));
    public static readonly PropertyConstraintType MinimumDate = new(nameof(MinimumDate));
    public static readonly PropertyConstraintType MinimumLength = new(nameof(MinimumLength));
    public static readonly PropertyConstraintType MinimumValue = new(nameof(MinimumValue));
    public static readonly PropertyConstraintType RequiresValue = new(nameof(RequiresValue));

    private PropertyConstraintType(string value) : base(value) { }

    public static PropertyConstraintType Parse(string value)
        => Parse(value, new[] { Case, EnforceListOfValues, MaximumDate, MaximumLength, MaximumValue, MinimumDate, MinimumLength, MinimumValue, RequiresValue });
}
