using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class VaultPropertyId : GenericId<long>, IEquatable<VaultPropertyId>, IComparable<VaultPropertyId>
{
    public VaultPropertyId(long value) : base(value) { }

    public static VaultPropertyId ParseFromAttribute(XElement element, string key)
        => new(long.TryParse(element.GetAttributeValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse property ID."));

    public int CompareTo(VaultPropertyId? other) => Value.CompareTo(other?.Value);
    public bool Equals(VaultPropertyId? other) => Value.Equals(other?.Value);
    public override bool Equals(object? obj) => obj is VaultPropertyId id && Equals(id);
    public override int GetHashCode() => Value.GetHashCode();
}
