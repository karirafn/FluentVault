
using FluentVault.Common;

namespace FluentVault;
public class VaultCategoryId : VaultGenericId<long>
{
    public VaultCategoryId(long value) : base(value) { }

    public static VaultCategoryId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse category ID."));
}
