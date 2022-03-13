
using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class MasterId : GenericId<long>
{
    public MasterId(long value) : base(value) { }

    public static MasterId ParseFromAttribute(XElement element, string key)
        => new(long.TryParse(element.GetAttributeValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse master ID."));
}
