using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.Common;
internal abstract class VaultEntitySerializer<T>
{
    public string ElementName { get; }
    public XNamespace Namespace { get; }
    protected XElement BaseElement => new(Namespace + ElementName);

    public VaultEntitySerializer(string elementName, XNamespace @namespace)
    {
        ElementName = elementName;
        Namespace = @namespace;
    }

    protected XElement GetSerializationElement(XElement element)
        => element.Name.LocalName == ElementName
            ? element
            : element.GetElement(ElementName);

    public abstract XElement Serialize(T entity);

    public IEnumerable<XElement> Serialize(IEnumerable<T>? entities)
        => entities?.Select(entity => Serialize(entity))
        ?? Enumerable.Empty<XElement>();

    public XElement SerializeMany(string name, IEnumerable<T> entities)
        => new XElement(Namespace + name)
            .AddElements(entities.Select(entity => Serialize(entity)));

    public abstract T Deserialize(XElement element);

    public IEnumerable<T> DeserializeMany(XElement element)
        => element.ParseAllElements(ElementName, Deserialize);
}
