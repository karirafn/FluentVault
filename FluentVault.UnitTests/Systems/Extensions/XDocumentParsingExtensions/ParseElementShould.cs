using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentParsingExtensions;

public class ParseElementShould
{
    [Fact]
    public void ReturnValue_WhenElementIsParsable()
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
