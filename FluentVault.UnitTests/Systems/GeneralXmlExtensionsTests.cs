using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using Xunit;

namespace FluentVault.UnitTests.Systems;

public class GeneralXmlExtensionsTests
{
    [Fact]
    public void GetElementValue_ShouldReturnValue_WhenDocumentContainsElement()
    {
        // Arrange
        var expectation = "someValue";
        var elementName = "SomeElement";
        var document = XDocument.Parse($"<{elementName}>{expectation}</{elementName}>");

        // Act
        var result = document.GetElementValue(elementName);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void GetElementValue_ShouldThrowException_WhenDocumentDoesNotContainElement()
    {
        // Arrange
        var elementToGet = "Something";
        var document = XDocument.Parse("<Root><Element>Value</Element></Root>");

        // Act

        // Assert
        Assert.Throws<KeyNotFoundException>(() => document.GetElementValue(elementToGet));
    }

    [Fact]
    public void GetElementValues_ShouldReturnValues_WhenDocumentContainsElements()
    {
        // Arrange
        var expectation = ("someValue", "someOtherValue");
        var firstElement = "FirstElement";
        var secondElement = "SecondElement";
        var document = XDocument.Parse($"<Root><{firstElement}>{expectation.Item1}</{firstElement}><{secondElement}>{expectation.Item2}</{secondElement}></Root>");

        // Act
        var result = document.GetElementValues(firstElement, secondElement);

        // Assert
        result.Should().Be(expectation);
    }
}
