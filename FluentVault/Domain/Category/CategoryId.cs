
using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class CategoryId : GenericId<long>
{
    public CategoryId(long value) : base(value) { }

    public static CategoryId ParseFromElement(XElement element, string key)
        => new(long.TryParse(element.GetElementValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse category ID."));

    public static CategoryId ParseFromAttribute(XElement element, string key)
        => new(long.TryParse(element.GetAttributeValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse category ID."));
}
