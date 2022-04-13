using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.Common;
public abstract class XDocumentParser<T> : XElementParser<T>
{
    private readonly string _elementName;

    public XDocumentParser(string elementName)
    {
        _elementName = elementName;
    }

    internal T ParseSingle(XDocument document)
        => document.ParseElement(_elementName, Parse);

    internal IEnumerable<T> ParseAll(XDocument document)
        => document.ParseAllElements(_elementName, Parse);
}
