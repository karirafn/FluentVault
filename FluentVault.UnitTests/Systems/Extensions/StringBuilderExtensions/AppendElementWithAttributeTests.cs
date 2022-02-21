using System.Text;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.StringBuilderExtensions;

public class AppendElementWithAttributeTests
{
    [Fact]
    public void AppendElementWithAttribute_ShouldReturnOpeningElement_WhenWhenIsSelfClosingIsNotDefined()
    {
        // Arrange
        string expectation = @"<Element Attribute=""Value"">";
        string elementName = "Element";
        string attributeName = "Attribute";
        string attributeValue = "Value";
        StringBuilder builder = new();

        // Act
        string result = builder.AppendElementWithAttribute(elementName, attributeName, attributeValue).ToString();

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void AppendElementWithAttribute_ShouldReturnOpeningElement_WhenWhenIsSelfClosingIsFalse()
    {
        // Arrange
        string expectation = @"<Element Attribute=""Value"">";
        string elementName = "Element";
        string attributeName = "Attribute";
        string attributeValue = "Value";
        bool isSelfClosing = false;
        StringBuilder builder = new();

        // Act
        string result = builder.AppendElementWithAttribute(elementName, attributeName, attributeValue, isSelfClosing).ToString();

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void AppendElementWithAttribute_ShouldReturnSelfClosingElement_WhenIsSelfClosingIsTrue()
    {
        // Arrange
        string expectation = @"<Element Attribute=""Value""/>";
        string elementName = "Element";
        string attributeName = "Attribute";
        string attributeValue = "Value";
        bool isSelfClosing = true;
        StringBuilder builder = new();

        // Act
        string result = builder.AppendElementWithAttribute(elementName, attributeName, attributeValue, isSelfClosing).ToString();

        // Assert
        result.Should().Be(expectation);
    }
}
