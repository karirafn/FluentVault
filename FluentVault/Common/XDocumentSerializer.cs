using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.Common;
internal abstract class XDocumentSerializer<T> : XElementSerializer<T>
{

    public XDocumentSerializer(string name, XNamespace @namespace) : base(name, @namespace)
    {
    }

    public T DeserializeSingle(XDocument document)
        => document.ParseElement(Name, Deserialize);

    public IEnumerable<T> DeserializeAll(XDocument document)
        => document.ParseAllElements(Name, Deserialize);
}
