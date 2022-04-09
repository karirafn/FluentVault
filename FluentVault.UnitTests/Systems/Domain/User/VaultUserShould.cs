
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultUserShould
{
    [Fact]
    public void ParseVaultUserFromXElement()
    {
        // Arrange
        (string body, VaultUser expectation) = VaultResponseFixtures.GetVaultUserFixture();
        XElement element = XElement.Parse(body);

        // Act
        VaultUser result = VaultUser.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
