using System.Text;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.StringBuilderExtensions;

public class AppendElementsTests
{
    [Fact]
    public void AppendElements_ShouldReturnElements_WhenValueIsString()
    {
        // Arrange
        string expectation = "<Element>A</Element><Element>B</Element>";
        string name = "Element";
        string[] values = new[] { "A", "B" };
        StringBuilder builder = new();

        // Act
        string result = builder.AppendElements(name, values).ToString();

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void AppendElements_ShouldReturnElements_WhenValueIsBool()
    {
        // Arrange
        string expectation = "<Element>true</Element><Element>false</Element>";
        string name = "Element";
        bool[] values = new[] { true, false };
        StringBuilder builder = new();

        // Act
        string result = builder.AppendElements(name, values).ToString();

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void AppendElements_ShouldReturnElements_WhenValueIsLong()
    {
        // Arrange
        string expectation = "<Element>56</Element><Element>666</Element>";
        string name = "Element";
        long[] values = new[] { 56L, 666L };
        StringBuilder builder = new();

        // Act
        string result = builder.AppendElements(name, values).ToString();

        // Assert
        result.Should().Be(expectation);
    }
}
