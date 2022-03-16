
using FluentVault.Common;

namespace FluentVault;
public class VaultUserId : VaultGenericId<long>
{
    public VaultUserId(long value) : base(value) { }

    public static VaultUserId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse user ID."));
}
