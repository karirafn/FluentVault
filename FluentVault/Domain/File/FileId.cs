
using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class FileId : GenericId<long>
{
    public FileId(long value) : base(value) { }

    public static FileId Parse(XElement element)
        => new(long.TryParse(element.GetElementValue("Id"), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse category ID."));
}
