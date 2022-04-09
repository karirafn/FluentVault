
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures.User;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultRoleShould
{
    private static readonly VaultRoleFixture _fixture = new(string.Empty);

    [Fact]
    public void ParseVaultRoleFromXElement()
    {
        // Arrange
        VaultRole expectation = _fixture.Create();
        XElement element = _fixture.ParseXElement(expectation);

        // Act
        VaultRole result = VaultRole.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
