using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.Common;
internal abstract class XDocumentSerializer<T>
{
    private readonly string _operation;
    private readonly XElementSerializer<T> _elementSerializer;

    public XDocumentSerializer(string operation, XElementSerializer<T> elementSerializer)
    {
        _operation = operation;
        _elementSerializer = elementSerializer;
    }

    public T Deserialize(XDocument document)
        => document.ParseElement(_elementSerializer.ElementName, _elementSerializer.Deserialize);

    public IEnumerable<T> DeserializeMany(XDocument document)
        => document.ParseAllElements(_elementSerializer.ElementName, _elementSerializer.Deserialize);

    public XDocument Serialize(T entity)
        => new XDocument().AddResponseContent(_operation, _elementSerializer.Namespace, _elementSerializer.Serialize(entity), null);

    public XDocument Serialize(IEnumerable<T> entity)
        => new XDocument().AddResponseContent(_operation, _elementSerializer.Namespace, _elementSerializer.Serialize(entity), null);
}
