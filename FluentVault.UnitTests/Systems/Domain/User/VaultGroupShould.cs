
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultGroupShould
{
    [Fact]
    public void ParseVaultGroupFromXDocument()
    {
        // Arrange
        var (body, expectation) = VaultResponseFixtures.GetVaultGroupFixture();
        var element = XElement.Parse(body);

        // Act
        var result = VaultGroup.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
