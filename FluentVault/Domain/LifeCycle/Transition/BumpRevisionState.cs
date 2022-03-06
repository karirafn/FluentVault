using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class BumpRevisionState : SmartEnum<BumpRevisionState>
{
    public static readonly BumpRevisionState BumpProperty = new(nameof(BumpProperty), 1);
    public static readonly BumpRevisionState BumpSecondary = new(nameof(BumpSecondary), 2);
    public static readonly BumpRevisionState BumpTertiary = new(nameof(BumpTertiary), 3);
    public static readonly BumpRevisionState None = new(nameof(None), 4);

    private BumpRevisionState(string name, int value) : base(name, value) { }
}
