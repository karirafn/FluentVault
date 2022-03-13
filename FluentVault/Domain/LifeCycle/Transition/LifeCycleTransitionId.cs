using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class LifeCycleTransitionId : GenericId<long>
{
    public LifeCycleTransitionId(long value) : base(value) { }

    public static LifeCycleTransitionId ParseFromAttribute(XElement element, string key)
        => new(long.TryParse(element.GetAttributeValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse life cycle transition ID."));
}
