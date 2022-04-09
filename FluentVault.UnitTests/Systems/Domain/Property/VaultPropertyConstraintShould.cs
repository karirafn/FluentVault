
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures.Property;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultPropertyConstraintShould
{
    private static readonly VaultPropertyConstraintFixture _fixture = new(string.Empty);

    [Fact]
    public void ParseVaultPropertyConstraintFromXElement()
    {
        // Arrange
        VaultPropertyConstraint expectation = _fixture.Create();
        XElement element = _fixture.ParseXElement(expectation);

        // Act
        VaultPropertyConstraint result = VaultPropertyConstraint.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
