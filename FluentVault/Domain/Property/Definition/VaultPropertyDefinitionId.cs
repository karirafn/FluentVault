
using FluentVault.Common;

namespace FluentVault;
public class VaultPropertyDefinitionId : VaultGenericId<long>, IEquatable<VaultPropertyDefinitionId>, IComparable<VaultPropertyDefinitionId>
{
    public VaultPropertyDefinitionId(long value) : base(value) { }

    public static VaultPropertyDefinitionId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse property ID."));

    public int CompareTo(VaultPropertyDefinitionId? other) => Value.CompareTo(other?.Value);
    public bool Equals(VaultPropertyDefinitionId? other) => Value.Equals(other?.Value);
    public override bool Equals(object? obj) => obj is VaultPropertyDefinitionId id && Equals(id);
    public override int GetHashCode() => Value.GetHashCode();
}
