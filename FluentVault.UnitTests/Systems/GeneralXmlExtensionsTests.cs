using System;
using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using Xunit;

namespace FluentVault.UnitTests.Systems;

public class GeneralXmlExtensionsTests
{
    [Fact]
    public void GetElementByNameFromDocument_ShouldReturnElement_WhenDocumentContainsElement()
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
    public void GetElementByNameFromDocument_ShouldThrowException_WhenDocumentDoesNotContainElement()
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
    public void GetElementByNameFromElement_ShouldReturnElement_WhenDocumentContainsElement()
    {
        // Arrange
        var name = "Nested";
        var value = "Value";
        var expectation = new XElement(name, value);
        var root = new XElement("Root", expectation);

        // Act
        var result = root.GetElementByName(name);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectation);
    }

    [Fact]
    public void GetElementByNameFromElement_ShouldThrowException_WhenDocumentDoesNotContainElement()
    {
        // Arrange
        var name = "NotThere";
        var nestedElement = new XElement("Nested", "Value");
        var root = new XElement("Root", nestedElement);

        // Act

        // Assert
        Assert.Throws<KeyNotFoundException>(() => root.GetElementByName(name));
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
    public void ParseAttributeAsLong_ShouldReturnValue_WhenValueCanBeParsed()
    {
        // Arrange
        var name = "Attribute";
        long expectation = 1234L;
        var attribute = new XAttribute(name, expectation.ToString());
        var root = new XElement("Root", attribute);

        // Act
        var result = root.ParseAttributeAsLong(name);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void ParseAttributeAsLong_ShouldThrowException_WhenValueCannotBeParsed()
    {
        // Arrange
        var name = "Attribute";
        var value = "NotANumber";
        var attribute = new XAttribute(name, value);
        var root = new XElement("Root", attribute);

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => root.ParseAttributeAsLong(name));
    }

    [Fact]
    public void ParseAttributeAsDateTime_ShouldReturnValue_WhenValueCanBeParsed()
    {
        // Arrange
        var name = "Attribute";
        var value = "2022-02-16T09:14:16.677+00:00";
        var expectation = new DateTime(2022, 2, 16, 9, 14, 16).AddMilliseconds(677);
        var attribute = new XAttribute(name, value);
        var root = new XElement("Root", attribute);

        // Act
        var result = root.ParseAttributeAsDateTime(name);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void ParseAttributeAsDateTime_ShouldThrowException_WhenValueCannotBeParsed()
    {
        // Arrange
        var name = "Attribute";
        var value = "NotADateTime";
        var attribute = new XAttribute(name, value);
        var root = new XElement("Root", attribute);

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => root.ParseAttributeAsDateTime(name));
    }

    [Fact]
    public void ParseAttributeAsBool_ShouldReturnValue_WhenValueCanBeParsed()
    {
        // Arrange
        var name = "Attribute";
        var value = "true";
        var expectation = true;
        var attribute = new XAttribute(name, value);
        var root = new XElement("Root", attribute);

        // Act
        var result = root.ParseAttributeAsBool(name);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void ParseAttributeAsBool_ShouldThrowException_WhenValueCannotBeParsed()
    {
        // Arrange
        var name = "Attribute";
        var value = "NotABool";
        var attribute = new XAttribute(name, value);
        var root = new XElement("Root", attribute);

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => root.ParseAttributeAsBool(name));
    }
}
