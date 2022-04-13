using System.Xml.Linq;

namespace FluentVault.Common;
public abstract class XElementParser<T>
{
    protected internal abstract T Parse(XElement element);
}
