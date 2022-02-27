using System;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentParsingExtensions;

public class ParseAttributeValueShould
{
    [Fact]
    public void ReturnValue_WhenAttributeIsParsable()
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
    public void ThrowException_WhenValueIsInvalid()
    {
        // Arrange
        string name = "Attribute";
        string xml = @"<Element Attribute=""Invalid""></Element>";
        XElement element = XElement.Parse(xml);

        // Act

        // Assert
        Assert.Throws<FormatException>(() => element.ParseAttributeValue(name, VaultFileStatus.Parse));
    }
}
