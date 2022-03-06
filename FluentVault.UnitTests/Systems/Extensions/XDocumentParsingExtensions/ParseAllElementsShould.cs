using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentParsingExtensions;

public class ParseAllElementsShould
{
    [Fact]
    public void ReturnValue_WhenElementsIsParsable()
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
