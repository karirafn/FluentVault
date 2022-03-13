
using FluentVault.Common;

namespace FluentVault;
public class VaultLifeCycleDefinitionId : GenericId<long>
{
    public VaultLifeCycleDefinitionId(long value) : base(value) { }

    public static VaultLifeCycleDefinitionId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse life cycle definition ID."));
}
