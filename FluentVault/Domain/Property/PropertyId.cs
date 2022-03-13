using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class PropertyId : GenericId<long>, IEquatable<PropertyId>, IComparable<PropertyId>
{
    public PropertyId(long value) : base(value) { }

    public static PropertyId ParseFromAttribute(XElement element, string key)
        => new(long.TryParse(element.GetAttributeValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse property ID."));

    public int CompareTo(PropertyId? other) => Value.CompareTo(other?.Value);
    public bool Equals(PropertyId? other) => Value.Equals(other?.Value);
    public override bool Equals(object? obj) => obj is PropertyId id && Equals(id);
    public override int GetHashCode() => Value.GetHashCode();
}
