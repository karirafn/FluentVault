
using FluentVault.Common;

namespace FluentVault;
public class VaultMasterId : VaultGenericId<long>
{
    public VaultMasterId(long value) : base(value) { }

    public static VaultMasterId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse master ID."));
}
