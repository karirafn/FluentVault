using System.Text;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.StringBuilderExtensions;

public class AppendNestedElementsTests
{
    [Fact]
    public void AppendNestedElements_ShouldReturnElements_WhenValuesAreStrings()
    {
        // Arrange
        string expectation = "<Root><Element>A</Element><Element>B</Element></Root>";
        string parentName = "Root";
        string childName = "Element";
        string[] values = new[] { "A", "B" };
        StringBuilder builder = new();

        // Act
        string result = builder.AppendNestedElements(parentName, childName, values).ToString();

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void AppendNestedElements_ShouldReturnElements_WhenValuesAreBools()
    {
        // Arrange
        string expectation = "<Root><Element>true</Element><Element>false</Element></Root>";
        string parentName = "Root";
        string childName = "Element";
        bool[] values = new[] { true, false };
        StringBuilder builder = new();

        // Act
        string result = builder.AppendNestedElements(parentName, childName, values).ToString();

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void AppendNestedElements_ShouldReturnElements_WhenValuesAreLongs()
    {
        // Arrange
        string expectation = "<Root><Element>69</Element><Element>666</Element></Root>";
        string parentName = "Root";
        string childName = "Element";
        long[] values = new[] { 69L, 666L };
        StringBuilder builder = new();

        // Act
        string result = builder.AppendNestedElements(parentName, childName, values).ToString();

        // Assert
        result.Should().Be(expectation);
    }
}
