using System.Collections.Generic;
using System.Text;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.StringBuilderExtensions;

public class AppendElementsWithAttributesTests
{
    [Fact]
    public void AppendElementsWithAttributes_ShouldReturnArrayOfSelfClosingElements_WhenInputIsValid()
    {
        // Arrange
        string expectation = @"<Element A=""1"" B=""2""/><Element A=""3"" B=""4""/>";
        string elementName = "Element";
        List<Dictionary<string, string>> attributesArray = new()
        {
            new() { { "A", "1" }, { "B", "2" } },
            new() { { "A", "3" }, { "B", "4" } }
        };

        StringBuilder builder = new();

        // Act
        string result = builder.AppendElementsWithAttributes(elementName, attributesArray).ToString();

        // Assert
        result.Should().Be(expectation);
    }
}
