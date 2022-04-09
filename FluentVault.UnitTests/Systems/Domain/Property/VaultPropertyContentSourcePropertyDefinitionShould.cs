
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures.Property;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultPropertyContentSourcePropertyDefinitionShould
{
    private static readonly VaultPropertyContentSourcePropertyDefinitionFixture _fixture = new(string.Empty);

    [Fact]
    public void ParseVaultPropertyContentSourcePropertyDefinitionFromXElement()
    {
        // Arrange
        VaultPropertyContentSourcePropertyDefinition expectation = _fixture.Create();
        XElement element = _fixture.ParseXElement(expectation);

        // Act
        VaultPropertyContentSourcePropertyDefinition result = VaultPropertyContentSourcePropertyDefinition.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
