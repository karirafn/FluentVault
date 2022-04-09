
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures.Property;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultPropertyEntityClassContentSourcePropertyDefinitionShould
{
    private static readonly VaultPropertyEntityClassContentSourcePropertyDefinitionFixture _fixture = new(string.Empty);

    [Fact]
    public void ParseVaultPropertyEntityClassContentSourcePropertyDefinitionFromXElement()
    {
        // Arrange
        VaultPropertyEntityClassContentSourcePropertyDefinition expectation = _fixture.Create();
        XElement element = _fixture.ParseXElement(expectation);

        // Act
        VaultPropertyEntityClassContentSourcePropertyDefinition result = VaultPropertyEntityClassContentSourcePropertyDefinition.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
