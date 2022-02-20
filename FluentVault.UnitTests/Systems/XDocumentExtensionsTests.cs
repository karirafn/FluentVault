using System;
using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems;

public class XDocumentExtensionsTests
{
    private record TestRecord(string Name);

    [Fact]
    public void GetElementValue_ShouldReturnValue_WhenTargetIsElement()
    {
        // Arrange
        string expectation = "Value";
        string name = "Element";
        string xml = "<Root><Element>Value</Element></Root>";
        XDocument document = XDocument.Parse(xml);

        // Act
        string result = document.GetElementValue(name);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void GetElementValue_ShouldThrowException_WhenElementDoesNotExist()
    {
        // Arrange
        string name = "Element";
        string xml = "<Root><Invalid>Value</Invalid></Root>";
        XDocument element = XDocument.Parse(xml);

        // Act

        // Assert
        Assert.Throws<KeyNotFoundException>(() => element.GetElementValue(name));
    }

    [Fact]
    public void GetAttributeValue_ShouldReturnValue_WhenElementContainsAttribute()
    {
        // Arrange
        string expectation = "Value";
        string name = "Attribute";
        string xml = @"<Element Attribute=""Value""/>";
        XElement element = XElement.Parse(xml);

        // Act
        string result = element.GetAttributeValue(name);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void GetAttributeValue_ShouldThrowException_WhenElementDoesNotContainAttribute()
    {
        // Arrange
        string name = "Invalid";
        string xml = $@"<Root><Element Attribute=""Value""/></Root>";
        XElement element = XElement.Parse(xml);

        // Act

        // Assert
        Assert.Throws<KeyNotFoundException>(() => element.GetAttributeValue(name));
    }

    [Fact]
    public void ParseElementValue_ShouldReturnValue_WhenValueIsParsable()
    {
        // Arrange
        int expectation = 50;
        string name = "Element";
        string xml = "<Root><Element>50</Element></Root>";
        XElement element = XElement.Parse(xml);

        // Act
        int result = element.ParseElementValue(name, int.Parse);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void ParseElementValue_ShouldThrowException_WhenValueIsInvalid()
    {
        // Arrange
        string name = "Element";
        string xml = "<Root><Element>NotANumber</Element></Root>";
        XElement element = XElement.Parse(xml);

        // Act

        // Assert
        Assert.Throws<FormatException>(() => element.ParseElementValue(name, int.Parse));
    }

    [Fact]
    public void ParseSingleElement_ShouldReturnValue_WhenElementIsParsable()
    {
        // Arrange
        TestRecord expectation = new("Value");
        string name = "Element";
        string xml = "<Root><Element>Value</Element></Root>";
        XDocument document = XDocument.Parse(xml);

        // Act
        TestRecord result = document.ParseSingleElement(name,
            element => new TestRecord(element.Value));

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }

    [Fact]
    public void ParseAttributeValue_ShouldReturnValue_WhenAttributeIsParsable()
    {
        // Arrange
        int expectation = 50;
        string name = "Attribute";
        string xml = @"<Element Attribute=""50""/>";
        XElement element = XElement.Parse(xml);

        // Act
        int result = element.ParseAttributeValue(name, int.Parse);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void ParseAttributeValue_ShouldThrowException_WhenValueIsInvalid()
    {
        // Arrange
        string name = "Attribute";
        string xml = @"<Element Attribute=""Invalid""></Element>";
        XElement element = XElement.Parse(xml);

        // Act

        // Assert
        Assert.Throws<FormatException>(() => element.ParseAttributeValue(name, VaultFileStatus.Parse));
    }

    [Fact]
    public void ParseAllElements_ShouldReturnValue_WhenElementsIsParsable()
    {
        // Arrange
        List<TestRecord> expectation = new() { new("A"), new("B") };
        string elementName = "Element";
        string attributeName = "Attribute";
        string xml = @"<Root><Element Attribute=""A""/><Element Attribute=""B""/></Root>";
        XDocument document = XDocument.Parse(xml);

        // Act
        IEnumerable<TestRecord> result = document.ParseAllElements(elementName,
            element => new TestRecord(element.GetAttributeValue(attributeName)));

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }

    [Fact]
    public void ParseAllElementValues_ShouldReturnValues_WhenValuesIsParsable()
    {
        // Arrange
        int[] expectation = new int[] { 1, 77 };
        string name = "Element";
        string xml = "<Root><Element>1</Element><Element>77</Element></Root>";
        XDocument document = XDocument.Parse(xml);

        // Act
        IEnumerable<int> result = document.ParseAllElementValues(name, int.Parse);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
