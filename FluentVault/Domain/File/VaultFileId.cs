
using FluentVault.Common;

namespace FluentVault;
public class VaultFileId : VaultGenericId<long>
{
    public VaultFileId(long value) : base(value) { }

    public static implicit operator VaultEntityId(VaultFileId fileId) => new(fileId.Value);

    public static VaultFileId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse category ID."));
}
