
using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class FileId : GenericId<long>
{
    private FileId(long value) : base(value) { }

    public static FileId Parse(XElement element)
        => new(long.TryParse(element.GetAttributeValue("Id"), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse category ID."));
}
