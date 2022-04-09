
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultPropertyContentSourcePropertyDefinitionShould
{
    [Fact]
    public void ParseVaultPropertyContentSourcePropertyDefinitionFromXElement()
    {
        // Arrange
        (string body, VaultProeprtyContentSourcePropertyDefinition expectation) = VaultResponseFixtures.GetVaultPropertyContentSourcePropertyDefinitionFixture();
        XElement element = XElement.Parse(body);

        // Act
        VaultProeprtyContentSourcePropertyDefinition result = VaultProeprtyContentSourcePropertyDefinition.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
