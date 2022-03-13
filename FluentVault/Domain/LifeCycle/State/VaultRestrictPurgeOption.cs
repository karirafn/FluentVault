using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultRestrictPurgeOption : SmartEnum<VaultRestrictPurgeOption>
{
    public static readonly VaultRestrictPurgeOption All = new(nameof(All), 1);
    public static readonly VaultRestrictPurgeOption FirstAndLast = new(nameof(FirstAndLast), 2);
    public static readonly VaultRestrictPurgeOption Last = new(nameof(Last), 3);
    public static readonly VaultRestrictPurgeOption None = new(nameof(None), 4);

    private VaultRestrictPurgeOption(string name, int value) : base(name, value) { }
}
