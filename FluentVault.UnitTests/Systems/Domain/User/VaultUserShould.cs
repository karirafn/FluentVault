
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures.User;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultUserShould
{
    private static readonly VaultUserFixture _fixture = new(string.Empty);

    [Fact]
    public void ParseVaultUserFromXElement()
    {
        // Arrange
        VaultUser expectation = _fixture.Create();
        XElement element = _fixture.ParseXElement(expectation);

        // Act
        VaultUser result = VaultUser.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
