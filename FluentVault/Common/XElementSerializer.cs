using System.Xml.Linq;

namespace FluentVault.Common;
internal abstract class XElementSerializer<T>
{
    protected XNamespace Namespace { get; }

    public XElementSerializer(XNamespace @namespace)
    {
        Namespace = @namespace;
    }

    internal abstract XElement Serialize(T entity);
    internal abstract T Deserialize(XElement element);
}
