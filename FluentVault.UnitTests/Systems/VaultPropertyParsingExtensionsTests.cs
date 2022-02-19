using System.Xml.Linq;

using FluentAssertions;

using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems;

public class VaultPropertyParsingExtensionsTests
{
    [Fact]
    public void ParseVaultProperty_ShouldReturnValidResult_WhenParsingValidString()
    {
        // Arrange
        var (body, expectation) = VaultResponseFixtures.GetVaultPropertyFixtures(5);
        var document = XDocument.Parse(body);

        // Act
        var result = document.ParseProperties();

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
