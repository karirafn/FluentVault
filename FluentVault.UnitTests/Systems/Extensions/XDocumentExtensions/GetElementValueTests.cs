using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentExtensions;

public class GetElementValueTests
{
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
}
