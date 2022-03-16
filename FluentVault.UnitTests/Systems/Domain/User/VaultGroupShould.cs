
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultGroupShould
{
    [Fact]
    public void ParseVaultGroupFromXElement()
    {
        // Arrange
        (string body, VaultGroup expectation) = VaultResponseFixtures.GetVaultGroupFixture();
        XElement element = XElement.Parse(body);

        // Act
        VaultGroup result = VaultGroup.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
