using System;
using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using Xunit;

namespace FluentVault.UnitTests.Systems;

public class GeneralXmlExtensionsTests
{
    [Fact]
    public void GetElementByName_ShouldReturnElement_WhenDocumentContainsElement()
    {
        // Arrange
        var name = "Nested";
        var value = "Value";
        var root = new XElement("Root");
        var expectation = new XElement(name, value);
        root.Add(expectation);

        var document = new XDocument();
        document.Add(root);

        // Act
        var result = document.GetElementByName(name);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectation);
    }

    [Fact]
    public void GetElementByName_ShouldThrowException_WhenDocumentDoesNotContainElement()
    {
        // Arrange
        var name = "NotThere";
        var root = new XElement("Root");
        var nestedElement = new XElement("Nested", "Value");
        root.Add(nestedElement);

        var document = new XDocument();
        document.Add(root);

        // Act

        // Assert
        Assert.Throws<KeyNotFoundException>(() => document.GetElementByName(name));
    }

    [Fact]
    public void GetAttributeValue_ShouldReturnValue_WhenElementContainsAttribute()
    {
        // Arrange
        var name = "Attribute";
        var expectation = "Value";
        var attribute = new XAttribute(name, expectation);
        var root = new XElement("Root", attribute);

        // Act
        var result = root.GetAttributeValue(name);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(expectation);
    }

    [Fact]
    public void GetAttributeValue_ShouldThrowException_WhenElementDoesNotContainAttribute()
    {
        // Arrange
        var name = "NotThere";
        var attribute = new XAttribute("Attribute", "Value");
        var root = new XElement("Root", attribute);

        // Act

        // Assert
        Assert.Throws<KeyNotFoundException>(() => root.GetAttributeValue(name));
    }

    [Fact]
    public void ParseSingleElement_ShouldReturnValue_WhenAttributeCanBeParsed()
    {
        // Arrange
        string attributeName = "Name";
        string attributeValue = "SomeValue";
        string elementName = "NestedElement";
        string elementValue = "NestedElement";

        TestRecord expectation = new(attributeValue);

        XAttribute attribute = new(attributeName, attributeValue);
        XElement nestedElement = new(elementName, elementValue);
        nestedElement.Add(attribute);

        XElement root = new("Root");
        root.Add(nestedElement);

        XDocument document = new();
        document.Add(root);

        // Act
        TestRecord result = document.ParseSingleElement(elementName,
            element => new TestRecord(element.GetAttributeValue(attributeName)));

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }

    [Fact]
    public void ParseAllElements_ShouldReturnValue_WhenAttributesCanBeParsed()
    {
        // Arrange
        var attributeValues = new string[] { "ValueA", "ValueB" };
        string attributeName = "Name";
        string elementName = "ElementName";
        string elementValue = "ElementValue";

        XElement root = new("Root");
        List<TestRecord> expectation = new();
        foreach (var value in attributeValues)
        {
            XAttribute attribute = new(attributeName, value);
            XElement element = new(elementName, elementValue);
            element.Add(attribute);
            root.Add(element);
            expectation.Add(new(value));
        }

        XDocument document = new();
        document.Add(root);

        // Act
        IEnumerable<TestRecord> result = document.ParseAllElements(elementName,
            element => new TestRecord(element.GetAttributeValue(attributeName)));

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}

internal record TestRecord(string Name);
