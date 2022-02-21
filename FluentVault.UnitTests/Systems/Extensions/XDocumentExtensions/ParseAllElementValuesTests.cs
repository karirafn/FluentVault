using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentExtensions;

public class ParseAllElementValuesTests
{
    [Fact]
    public void ParseAllElementValues_ShouldReturnValues_WhenValuesIsParsable()
    {
        // Arrange
        int[] expectation = new int[] { 1, 77 };
        string name = "Element";
        string xml = "<Root><Element>1</Element><Element>77</Element></Root>";
        XDocument document = XDocument.Parse(xml);

        // Act
        IEnumerable<int> result = document.ParseAllElementValues(name, int.Parse);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
