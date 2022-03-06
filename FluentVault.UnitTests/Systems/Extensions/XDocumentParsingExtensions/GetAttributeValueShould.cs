using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentParsingExtensions;

public class GetAttributeValueShould
{
    [Fact]
    public void ReturnValue_WhenElementContainsAttribute()
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
    public void ThrowException_WhenElementDoesNotContainAttribute()
    {
        // Arrange
        string name = "Invalid";
        string xml = $@"<Root><Element Attribute=""Value""/></Root>";
        XElement element = XElement.Parse(xml);

        // Act

        // Assert
        Assert.Throws<KeyNotFoundException>(() => element.GetAttributeValue(name));
    }
}
