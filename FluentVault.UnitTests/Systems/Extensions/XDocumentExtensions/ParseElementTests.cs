using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentExtensions;

public class ParseElementTests
{
    [Fact]
    public void ParseSingleElement_ShouldReturnValue_WhenElementIsParsable()
    {
        // Arrange
        TestRecord expectation = new("Value");
        string name = "Element";
        string xml = "<Root><Element>Value</Element></Root>";
        XDocument document = XDocument.Parse(xml);

        // Act
        TestRecord result = document.ParseElement(name,
            element => new TestRecord(element.Value));

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
