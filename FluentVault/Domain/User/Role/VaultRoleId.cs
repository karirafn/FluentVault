
using FluentVault.Common;

namespace FluentVault;
public class VaultRoleId : VaultGenericId<long>
{
    public VaultRoleId(long value) : base(value) { }

    public static VaultRoleId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse role ID."));
}
