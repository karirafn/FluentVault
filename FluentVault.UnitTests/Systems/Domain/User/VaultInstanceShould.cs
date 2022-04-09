
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures.User;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultInstanceShould
{
    private static readonly VaultGroupFixture _fixture = new(string.Empty);

    [Fact]
    public void ParseVaultInstanceFromXElement()
    {
        // Arrange
        VaultGroup expectation = _fixture.Create();
        XElement element = _fixture.ParseXElement(expectation);

        // Act
        VaultGroup result = VaultGroup.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
