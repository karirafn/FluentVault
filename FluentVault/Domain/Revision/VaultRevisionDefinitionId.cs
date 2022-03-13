
using FluentVault.Common;

namespace FluentVault;
public class VaultRevisionDefinitionId : GenericId<long>
{
    public VaultRevisionDefinitionId(long value) : base(value) { }

    public static VaultRevisionDefinitionId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse revision definition ID."));
}
