
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultPropertyEntityClassContentSourcePropertyDefinitionShould
{
    [Fact]
    public void ParseVaultPropertyEntityClassContentSourcePropertyDefinitionFromXElement()
    {
        // Arrange
        (string body, VaultPropertyEntityClassContentSourcePropertyDefinition expectation) = VaultResponseFixtures.GetVaultPropertyEntityClassContentSourcePropertyDefinitionFixture();
        XElement element = XElement.Parse(body);

        // Act
        VaultPropertyEntityClassContentSourcePropertyDefinition result = VaultPropertyEntityClassContentSourcePropertyDefinition.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
