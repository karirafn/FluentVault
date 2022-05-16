
using FluentVault.Common;

namespace FluentVault;
public class VaultEntityId : VaultGenericId<long>
{
    public VaultEntityId(long value) : base(value) { }

    public static implicit operator VaultFileId(VaultEntityId entityId) => new(entityId.Value);
    public static implicit operator VaultItemId(VaultEntityId entityId) => new(entityId.Value);
    public static implicit operator VaultFolderId(VaultEntityId entityId) => new(entityId.Value);

    public static VaultEntityId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse entity ID."));
}
