using System.Collections.Generic;
using System.Text;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.StringBuilderExtensions;

public class AppendElementWithAttributesTests
{
    [Fact]
    public void AppendElementWithAttributes_ShouldReturnOpeningElement_WhenIsSelfClosingIsUndefined()
    {
        // Arrange
        string expectation = @"<Element A=""1"" B=""2"">";
        string elementName = "Element";
        Dictionary<string, string> attributes = new() { { "A", "1" }, { "B", "2" } };
        StringBuilder builder = new();

        // Act
        string result = builder.AppendElementWithAttributes(elementName, attributes).ToString();

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void AppendElementWithAttributes_ShouldReturnOpeningElement_WhenIsSelfClosingIsFalse()
    {
        // Arrange
        string expectation = @"<Element A=""1"" B=""2"">";
        string elementName = "Element";
        Dictionary<string, string> attributes = new() { { "A", "1" }, { "B", "2" } };
        bool isSelfClosing = false;
        StringBuilder builder = new();

        // Act
        string result = builder.AppendElementWithAttributes(elementName, attributes, isSelfClosing).ToString();

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void AppendElementWithAttributes_ShouldReturnSelfClosingElement_WhenSelfClosingIsTrue()
    {
        // Arrange
        string expectation = @"<Element A=""1"" B=""2""/>";
        string elementName = "Element";
        Dictionary<string, string> attributes = new() { { "A", "1" }, { "B", "2" } };
        bool isSelfClosing = true;
        StringBuilder builder = new();

        // Act
        string result = builder.AppendElementWithAttributes(elementName, attributes, isSelfClosing).ToString();

        // Assert
        result.Should().Be(expectation);
    }
}
