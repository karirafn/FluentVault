using System.Xml.Linq;

namespace FluentVault;

internal static class GeneralXDocumentExtensions
{
    internal static string GetElementValue(this XDocument document, string name)
        => document.Descendants()
            .FirstOrDefault(x => x.Name.LocalName.Equals(name))?
            .Value
        ?? throw new KeyNotFoundException($"Failed to get element with name {name}.");

    internal static (string, string) GetElementValues(this XDocument document, string a, string b)
        => (document.GetElementValue(a), document.GetElementValue(b));

    internal static XElement GetElementByName(this XDocument document, string name)
        => document.Descendants()
            .FirstOrDefault(x => x.Name.LocalName.Equals(name))
        ?? throw new KeyNotFoundException($"Failed to get element with name {name}.");

    internal static string GetAttributeValue(this XElement element, string name)
        => element.Attribute(name)?.Value
        ?? throw new KeyNotFoundException($"Attribute {name} was not found.");

    internal static XElement GetElementByName(this XElement element, string name)
        => element.Descendants().FirstOrDefault(x => x.Name.LocalName.Equals(name))
        ?? throw new KeyNotFoundException($"Nested element {name} was not found in element {element.Name}");

    internal static long ParseAttributeAsLong(this XElement element, string name)
        => long.TryParse(element.GetAttributeValue(name), out long value)
        ? value
        : throw new ArgumentException($@"Failed to parse attribute ""{name}"" as type long");

    internal static bool ParseAttributeAsBool(this XElement element, string name)
        => bool.TryParse(element.GetAttributeValue(name), out bool value)
        ? value
        : throw new ArgumentException($@"Failed to parse attribute ""{name}"" as type bool");

    internal static DateTime ParseAttributeAsDateTime(this XElement element, string name)
        => DateTime.TryParse(element.GetAttributeValue(name), out DateTime value)
        ? value
        : throw new ArgumentException($@"Failed to parse attribute ""{name}"" as type DateTime");
}
