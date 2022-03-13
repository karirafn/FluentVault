
using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class VaultCategoryId : GenericId<long>
{
    public VaultCategoryId(long value) : base(value) { }

    public static VaultCategoryId ParseFromElement(XElement element, string key)
        => new(long.TryParse(element.GetElementValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse category ID."));

    public static VaultCategoryId ParseFromAttribute(XElement element, string key)
        => new(long.TryParse(element.GetAttributeValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse category ID."));
}
