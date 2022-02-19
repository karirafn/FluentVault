using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Domain.Lifecycle;
using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems;

public class VaultLifecycleParsingExtensionsTests
{
    [Fact]
    public void ParseVaultLifecycle_ShouldReturnValidResult_WhenParsingValidString()
    {
        // Arrange
        var (body, expectation) = VaultResponseFixtures.GetVaultLifecycleFixtures(5);
        var document = XDocument.Parse(body);

        // Act
        var result = document.ParseLifecycles();

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
