using System.Xml.Linq;

namespace FluentVault;

internal static class XDocumentExtensions
{
    internal static XElement GetElementByName(this XContainer container, string name)
        => container.Descendants().FirstOrDefault(x => x.Name.LocalName.Equals(name))
        ?? throw new KeyNotFoundException($@"Element ""{name}"" was not found");

    internal static string GetAttributeValue(this XElement element, string name)
        => element.Attribute(name)?.Value
        ?? throw new KeyNotFoundException($@"Attribute ""{name}"" was not found in element ""{element.Name}"".");

    internal static T ParseAttribute<T>(this XElement element, string name, Func<string, T> parse)
        => parse(element.GetAttributeValue(name));

    internal static T ParseSingleElement<T>(this XContainer container, string name, Func<XElement, T> parse)
        => parse(container.GetElementByName(name));

    internal static IEnumerable<T> ParseAllElements<T>(this XContainer container, string name, Func<XElement, T> parse)
        => container.Descendants()
            .Where(x => x.Name.LocalName.Equals(name))
            .Select(x => parse(x));

}
