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
        var name = "Inner";
        var value = "Value";
        var expectation = new XElement(name, value);
        var outerElement = new XElement("Outer", expectation);

        // Act
        var result = outerElement.GetElementByName(name);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectation);
    }

    [Fact]
    public void GetElementByName_ShouldThrowException_WhenDocumentDoesNotContainElement()
    {
        // Arrange
        var name = "NotThere";
        var outerElement = new XElement("Outer", new XElement("Inner", "Value"));

        // Act

        // Assert
        Assert.Throws<KeyNotFoundException>(() => outerElement.GetElementByName(name));
    }
}
