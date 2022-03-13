
using FluentVault.Common;

namespace FluentVault;
public class VaultLifeCycleStateId : GenericId<long>
{
    public VaultLifeCycleStateId(long value) : base(value) { }

    public static VaultLifeCycleStateId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse life cycle state ID."));
}
