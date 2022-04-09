﻿using System.Xml.Linq;

namespace FluentVault.Extensions;

internal static class XDocumentParsingExtensions
{
    internal static string GetElementValue(this XContainer container, string name)
        => container.GetSingleElementByName(name).Value;

    internal static T ParseElementValue<T>(this XElement element, string name, Func<string, T> parse)
        => ParseValue(name, element.GetElementValue(name), parse);

    internal static T ParseAttributeValue<T>(this XElement element, string name, Func<string, T> parse)
        => ParseValue(name, element.GetAttributeValue(name), parse);

    internal static T ParseElement<T>(this XContainer container, string name, Func<XElement, T> parse)
        => parse(container.GetSingleElementByName(name));

    internal static IEnumerable<T> ParseAllElements<T>(this XContainer container, string name, Func<XElement, T> parse)
        => container.GetAllElementsByName(name).Select(x => parse(x));

    internal static IEnumerable<T> ParseAllElementValues<T>(this XContainer container, string name, Func<string, T> parse)
        => container.GetAllElementsByName(name).Select(x => parse(x.Value));

    internal static string GetAttributeValue(this XElement element, string name)
        => element.Attribute(name)?.Value
        ?? throw new KeyNotFoundException($@"Attribute ""{name}"" was not found in element ""{element.Name}"".");

    private static XElement GetSingleElementByName(this XContainer container, string name)
        => container.Descendants().FirstOrDefault(x => x.Name.LocalName.Equals(name))
        ?? throw new KeyNotFoundException($@"Element ""{name}"" was not found");

    private static IEnumerable<XElement> GetAllElementsByName(this XContainer container, string name)
        => container.Descendants().Where(x => x.Name.LocalName.Equals(name));

    private static T ParseValue<T>(string name, string value, Func<string, T> parse)
    {
        try { return parse(value); }
        catch (Exception e) { throw new FormatException(@$"Failed to parse ""{name}"" to type ""{typeof(T)}""", e.InnerException); }
    }
}
