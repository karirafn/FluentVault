
using FluentVault.Common;

namespace FluentVault;
public class VaultFolderId : VaultGenericId<long>
{
    public VaultFolderId(long value) : base(value) { }

    public static VaultFolderId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse folder ID."));
}
