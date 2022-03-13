using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class VaultPropertyDefinitionId : GenericId<long>, IEquatable<VaultPropertyDefinitionId>, IComparable<VaultPropertyDefinitionId>
{
    public VaultPropertyDefinitionId(long value) : base(value) { }

    public static VaultPropertyDefinitionId ParseFromAttribute(XElement element, string key)
        => new(long.TryParse(element.GetAttributeValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse property ID."));

    public int CompareTo(VaultPropertyDefinitionId? other) => Value.CompareTo(other?.Value);
    public bool Equals(VaultPropertyDefinitionId? other) => Value.Equals(other?.Value);
    public override bool Equals(object? obj) => obj is VaultPropertyDefinitionId id && Equals(id);
    public override int GetHashCode() => Value.GetHashCode();
}
