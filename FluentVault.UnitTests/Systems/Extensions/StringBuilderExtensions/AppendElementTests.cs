using System.Text;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.StringBuilderExtensions;

public class AppendElementTests
{
    [Fact]
    public void AppendElement_ShouldReturnElement_WhenValueIsString()
    {
        // Arrange
        string expectation = "<Element>Value</Element>";
        string name = "Element";
        string value = "Value";
        StringBuilder builder = new();

        // Act
        string result = builder.AppendElement(name, value).ToString();

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void AppendElement_ShouldReturnElement_WhenValueIsBool()
    {
        // Arrange
        string expectation = "<Element>true</Element>";
        string name = "Element";
        bool value = true;
        StringBuilder builder = new();

        // Act
        string result = builder.AppendElement(name, value).ToString();

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void AppendElement_ShouldReturnElement_WhenValueIsLong()
    {
        // Arrange
        string expectation = "<Element>666</Element>";
        string name = "Element";
        long value = 666;
        StringBuilder builder = new();

        // Act
        string result = builder.AppendElement(name, value).ToString();

        // Assert
        result.Should().Be(expectation);
    }
}
