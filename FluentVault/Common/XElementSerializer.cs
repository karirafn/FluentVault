using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.Common;
internal abstract class XElementSerializer<T>
{
    public string ElementName { get; }
    public XNamespace Namespace { get; }
    protected XElement BaseElement => new(Namespace + ElementName);

    public XElementSerializer(string elementName, XNamespace @namespace)
    {
        ElementName = elementName;
        Namespace = @namespace;
    }

    protected XElement GetSerializationElement(XElement element)
        => element.Name.LocalName == ElementName
            ? element
            : element.GetElement(ElementName);

    internal abstract XElement Serialize(T entity);
    
    internal IEnumerable<XElement> Serialize(IEnumerable<T> entities)
        => entities.Select(entity => Serialize(entity));
    
    internal XElement SerializeMany(string name, IEnumerable<T> entities)
        => new XElement(Namespace + name)
            .AddElements(entities.Select(entity => Serialize(entity)));

    internal abstract T Deserialize(XElement element);
    
    internal IEnumerable<T> DeserializeMany(XElement element)
        => element.ParseAllElements(ElementName, Deserialize);
}
