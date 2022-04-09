
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultInstanceShould
{
    [Fact]
    public void ParseVaultInstanceFromXElement()
    {
        // Arrange
        (string body, VaultInstance expectation) = VaultResponseFixtures.GetVaultInstanceFixture();
        XElement element = XElement.Parse(body);

        // Act
        VaultInstance result = VaultInstance.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
