
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultPropertyConstraintShould
{
    [Fact]
    public void ParseVaultPropertyConstraintFromXElement()
    {
        // Arrange
        (string body, VaultPropertyConstraint expectation) = VaultResponseFixtures.GetVaultPropertyConstraintFixture();
        XElement element = XElement.Parse(body);

        // Act
        VaultPropertyConstraint result = VaultPropertyConstraint.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
