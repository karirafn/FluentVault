
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultRoleShould
{
    [Fact]
    public void ParseVaultRoleFromXDocument()
    {
        // Arrange
        var (body, expectation) = VaultResponseFixtures.GetVaultRoleFixture();
        var element = XElement.Parse(body);

        // Act
        var result = VaultRole.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
