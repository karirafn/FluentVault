
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultRoleShould
{
    [Fact]
    public void ParseVaultRoleFromXElement()
    {
        // Arrange
        (string body, VaultRole expectation) = VaultResponseFixtures.GetVaultRoleFixture();
        XElement element = XElement.Parse(body);

        // Act
        VaultRole result = VaultRole.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
