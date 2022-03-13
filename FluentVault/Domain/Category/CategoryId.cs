
using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class CategoryId : GenericId<long>
{
    public CategoryId(long value) : base(value) { }

    public static CategoryId Parse(XElement element)
        => new(long.TryParse(element.GetElementValue("Id"), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse category ID."));
}
