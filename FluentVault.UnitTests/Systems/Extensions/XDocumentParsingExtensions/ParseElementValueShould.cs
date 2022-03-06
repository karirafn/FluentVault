using System;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentParsingExtensions;

public class ParseElementValueShould
{
    [Fact]
    public void ReturnValue_WhenValueIsParsable()
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
    public void ThrowException_WhenValueIsInvalid()
    {
        // Arrange
        string name = "Element";
        string xml = "<Root><Element>NotANumber</Element></Root>";
        XElement element = XElement.Parse(xml);

        // Act

        // Assert
        Assert.Throws<FormatException>(() => element.ParseElementValue(name, int.Parse));
    }
}
