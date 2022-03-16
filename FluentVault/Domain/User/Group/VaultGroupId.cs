using FluentVault.Common;

namespace FluentVault;
public class VaultGroupId : VaultGenericId<long>
{
    public VaultGroupId(long value) : base(value) { }

    public static VaultGroupId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse group ID."));
}
