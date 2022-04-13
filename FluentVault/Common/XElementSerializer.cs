using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.Common;
internal abstract class XElementSerializer<T>
{
    protected string Name { get; }
    protected XNamespace Namespace { get; }
    protected XElement BaseElement { get; }

    public XElementSerializer(string name, XNamespace @namespace)
    {
        Name = name;
        Namespace = @namespace;
        BaseElement = new(Namespace + Name);
    }

    internal abstract XElement Serialize(T entity);
    internal abstract T Deserialize(XElement element);
    internal IEnumerable<T> DeserializeMany(XElement element)
        => element.ParseAllElements(Name, Deserialize);
    internal IEnumerable<XElement> Serialize(IEnumerable<T> entities)
        => entities.Select(entity => Serialize(entity));
}
