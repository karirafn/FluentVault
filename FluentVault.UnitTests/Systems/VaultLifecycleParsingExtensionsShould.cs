using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Domain.Lifecycle;
using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems;

public class VaultLifecycleParsingExtensionsShould
{
    [Fact]
    public void ReturnValidResult_WhenParsingValidString()
    {
        // Arrange
        var (body, expectation) = VaultResponseFixtures.GetVaultLifecycleFixtures(5);
        var document = XDocument.Parse(body);

        // Act
        var result = document.ParseLifeCycles();

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
