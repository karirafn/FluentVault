
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultFileLifeCycleShould
{
    [Fact]
    public void ParseVaultFileLifeCycleFromXElement()
    {
        // Arrange
        (string body, VaultFileLifeCycle expectation) = VaultResponseFixtures.GetVaultFileLifeCycleFixture();
        XElement element = XElement.Parse(body);

        // Act
        VaultFileLifeCycle result = VaultFileLifeCycle.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
