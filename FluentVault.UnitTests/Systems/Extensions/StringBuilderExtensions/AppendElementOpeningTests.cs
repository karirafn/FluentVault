using System.Text;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.StringBuilderExtensions;

public class AppendElementOpeningTests
{
    [Fact]
    public void AppendElementOpening_ShouldReturnOpeningTag_WhenInputIsValid()
    {
        // Arrange
        string expectation = "<Element>";
        string name = "Element";
        StringBuilder builder = new();

        // Act
        string result = builder.AppendElementOpening(name).ToString();

        // Assert
        result.Should().Be(expectation);
    }
}
