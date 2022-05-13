using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.Common;
internal abstract class XDocumentSerializer<T>
{
    private readonly string _operation;

    public XDocumentSerializer(string operation, XElementSerializer<T> entitySerializer)
    {
        _operation = operation;
        ElementSerializer = entitySerializer;
    }

    public XElementSerializer<T> ElementSerializer { get; }

    public T Deserialize(XDocument document)
        => document.ParseElement(ElementSerializer.ElementName, ElementSerializer.Deserialize);

    public IEnumerable<T> DeserializeMany(XDocument document)
        => document.ParseAllElements(ElementSerializer.ElementName, ElementSerializer.Deserialize);

    public virtual XDocument Serialize(T entity)
        => new XDocument().AddResponseContent(_operation, ElementSerializer.Namespace, ElementSerializer.Serialize(entity), null);

    public XDocument Serialize(IEnumerable<T> entity)
        => new XDocument().AddResponseContent(_operation, ElementSerializer.Namespace, ElementSerializer.Serialize(entity), null);
}
