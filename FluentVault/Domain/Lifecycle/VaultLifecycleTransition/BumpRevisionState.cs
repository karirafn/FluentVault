using FluentVault.Common;

namespace FluentVault;

public class BumpRevisionState : BaseType
{
    public static readonly BumpRevisionState BumpProperty = new(nameof(BumpProperty));
    public static readonly BumpRevisionState BumpSecondary = new(nameof(BumpSecondary));
    public static readonly BumpRevisionState BumpTertiary = new(nameof(BumpTertiary));
    public static readonly BumpRevisionState None = new(nameof(None));

    private BumpRevisionState(string value) : base(value) { }

    public static BumpRevisionState Parse(string value)
        => Parse(value, new[] { BumpProperty, BumpSecondary, BumpTertiary, None });
}
