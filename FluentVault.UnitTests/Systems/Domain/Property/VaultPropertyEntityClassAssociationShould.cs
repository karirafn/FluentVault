
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultPropertyEntityClassAssociationShould
{
    [Fact]
    public void ParseVaultEntityClassAssociationFromXElement()
    {
        // Arrange
        (string body, VaultPropertyEntityClassAssociation expectation) = VaultResponseFixtures.GetVaultPropertyEntityClassAssociationFixture();
        XElement element = XElement.Parse(body);

        // Act
        VaultPropertyEntityClassAssociation result = VaultPropertyEntityClassAssociation.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
