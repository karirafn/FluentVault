using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.Common;
internal abstract class XDocumentSerializer<T> : XElementSerializer<T>
{
    private readonly string _name;

    public XDocumentSerializer(string name, XNamespace @namespace) : base(@namespace)
    {
        _name = name;
    }

    public T DeserializeSingle(XDocument document)
        => document.ParseElement(_name, Deserialize);

    public IEnumerable<T> DeserializeAll(XDocument document)
        => document.ParseAllElements(_name, Deserialize);
}
