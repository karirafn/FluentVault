using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentExtensions;

public class ParseAllElementsTests
{
    [Fact]
    public void ParseAllElements_ShouldReturnValue_WhenElementsIsParsable()
    {
        // Arrange
        List<TestRecord> expectation = new() { new("A"), new("B") };
        string elementName = "Element";
        string attributeName = "Attribute";
        string xml = @"<Root><Element Attribute=""A""/><Element Attribute=""B""/></Root>";
        XDocument document = XDocument.Parse(xml);

        // Act
        IEnumerable<TestRecord> result = document.ParseAllElements(elementName,
            element => new TestRecord(element.GetAttributeValue(attributeName)));

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
