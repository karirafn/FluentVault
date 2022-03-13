
using FluentVault.Common;

namespace FluentVault;
public class VaultLifeCycleTransitionId : VaultGenericId<long>
{
    public VaultLifeCycleTransitionId(long value) : base(value) { }

    public static VaultLifeCycleTransitionId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse life cycle transition ID."));
}
