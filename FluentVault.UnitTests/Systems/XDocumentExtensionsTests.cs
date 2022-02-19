using System;
using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems;

public class XDocumentExtensionsTests
{
    [Fact]
    public void GetElementValue_ShouldReturnValue_WhenElementExists()
    {
        // Arrange
        string name = "Element";
        string expectation = "Value";
        XElement element = new (name, expectation);
        XElement root = new ("Root");
        root.Add(element);

        // Act
        string result = root.GetElementValue(name);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void GetElementValue_ShouldThrowException_WhenElementDoesNotExist()
    {
        // Arrange
        string name = "NotThere";
        XElement element = new ("Element", "Value");
        XElement root = new ("Root");
        root.Add(element);

        // Act

        // Assert
        Assert.Throws<KeyNotFoundException>(() => root.GetElementValue(name));
    }

    [Fact]
    public void GetAttributeValue_ShouldReturnValue_WhenElementContainsAttribute()
    {
        // Arrange
        string name = "Attribute";
        string expectation = "Value";
        XAttribute attribute = new (name, expectation);
        XElement root = new ("Root", attribute);

        // Act
        string result = root.GetAttributeValue(name);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void GetAttributeValue_ShouldThrowException_WhenElementDoesNotContainAttribute()
    {
        // Arrange
        string name = "NotThere";
        XAttribute attribute = new ("Attribute", "Value");
        XElement root = new ("Root", attribute);

        // Act

        // Assert
        Assert.Throws<KeyNotFoundException>(() => root.GetAttributeValue(name));
    }

    [Fact]
    public void ParseElementValue_ShouldReturnValue_WhenValueIsParsable()
    {
        // Arrange
        string name = "Element";
        int expectation = 50;
        XElement element = new (name, expectation);
        XElement root = new ("Root");
        root.Add(element);

        // Act
        int result = root.ParseElementValue(name, int.Parse);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void ParseElementValue_ShouldThrowException_WhenValueIsInvalid()
    {
        // Arrange
        string name = "Element";
        XElement element = new (name, "NotANumber");
        XElement root = new ("Root");
        root.Add(element);

        // Act

        // Assert
        Assert.Throws<FormatException>(() => root.ParseElementValue(name, int.Parse));
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
    public void ParseAttributeValue_ShouldReturnValue_WhenValueIsValid()
    {
        // Arrange
        string name = "Attribute";
        int expectation = 50;
        XAttribute attribute = new(name, expectation);
        XElement root = new("Root");
        root.Add(attribute);

        // Act
        int result = root.ParseAttributeValue(name, int.Parse);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void ParseAttributeValue_ShouldThrowException_WhenValueIsInvalid()
    {
        // Arrange
        string name = "Attribute";
        XAttribute attribute = new(name, "NotAFileStatus");
        XElement root = new("Root");
        root.Add(attribute);

        // Act

        // Assert
        Assert.Throws<FormatException>(() => root.ParseAttributeValue(name, VaultFileStatus.Parse));
    }

    [Fact]
    public void ParseAllElements_ShouldReturnValue_WhenElementsCanBeParsed()
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

    [Fact]
    public void ParseAllElementValues_ShouldReturnValues_WhenValuesCanBeParsed()
    {
        // Arrange
        var expectation = new int[] { 1, 77 };
        string name = "Element";

        XElement root = new("Root");
        foreach (var value in expectation)
        {
            XElement element = new(name, value);
            root.Add(element);
        }

        XDocument document = new();
        document.Add(root);

        // Act
        IEnumerable<int> result = document.ParseAllElementValues(name, int.Parse);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}

internal record TestRecord(string Name);
