using System.Text;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.StringBuilderExtensions;

public class AppendElementClosingTests
{
    [Fact]
    public void AppendElementClosing_ShouldReturnClosingElement_WhenInputIsValid()
    {
        // Arrange
        string expectation = "</Element>";
        string name = "Element";
        StringBuilder builder = new();

        // Act
        string result = builder.AppendElementClosing(name).ToString();

        // Assert
        result.Should().Be(expectation);
    }
}
