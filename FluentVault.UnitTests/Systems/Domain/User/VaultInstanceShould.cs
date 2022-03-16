
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultInstanceShould
{
    [Fact]
    public void ParseVaultInstanceFromXDocument()
    {
        // Arrange
        var (body, expectation) = VaultResponseFixtures.GetVaultInstanceFixture();
        var element = XElement.Parse(body);

        // Act
        var result = VaultInstance.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
