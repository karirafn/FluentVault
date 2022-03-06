using System.Xml.Linq;

using FluentAssertions;

using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Doman;

public class VaultLifecycleShould
{
    [Fact]
    public void ParseLifeCyclesFromXDocument()
    {
        // Arrange
        var (body, expectation) = VaultResponseFixtures.GetVaultLifecycleFixtures(5);
        var document = XDocument.Parse(body);

        // Act
        var result = VaultLifeCycle.ParseAll(document);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
