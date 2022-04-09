
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures.Property;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultPropertyEntityClassAssociationShould
{
    private static readonly VaultPropertyEntityClassAssociationFixture _fixture = new(string.Empty);

    [Fact]
    public void ParseVaultEntityClassAssociationFromXElement()
    {
        // Arrange
        VaultPropertyEntityClassAssociation expectation = _fixture.Create();
        XElement element = _fixture.ParseXElement(expectation);

        // Act
        VaultPropertyEntityClassAssociation result = VaultPropertyEntityClassAssociation.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
