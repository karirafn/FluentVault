using System.Collections.Generic;
using System.Text;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.StringBuilderExtensions;

public class AppendNestedElementsWithAttributesTests
{
    [Fact]
    public void AppendNestedElementsWithAttributes_ShouldReturnNestedArrayOfSelfClosingElements_WhenInputIsValid()
    {
        // Arrange
        string expectation = @"<Root><Element A=""1"" B=""2""/><Element A=""3"" B=""4""/></Root>";
        string parentName = "Root";
        string childName = "Element";
        List<Dictionary<string, string>> attributesArray = new()
        {
            new() { { "A", "1" }, { "B", "2" } },
            new() { { "A", "3" }, { "B", "4" } }
        };

        StringBuilder builder = new();

        // Act
        string result = builder.AppendNestedElementsWithAttributes(parentName, childName, attributesArray).ToString();

        // Assert
        result.Should().Be(expectation);
    }
}
