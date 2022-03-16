
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultUserShould
{
    [Fact]
    public void ParseVaultUserFromXDocument()
    {
        // Arrange
        var (body, expectation) = VaultResponseFixtures.GetVaultUserFixture();
        var element = XElement.Parse(body);

        // Act
        var result = VaultUser.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
