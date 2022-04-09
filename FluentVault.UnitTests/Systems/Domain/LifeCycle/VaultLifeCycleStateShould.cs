
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultLifeCycleStateShould
{
    [Fact]
    public void ParseVaultLifeCycleStateFromXElement()
    {
        // Arrange
        (string body, VaultLifeCycleState expectation) = VaultResponseFixtures.GetVaultLifeCycleStateFixture();
        XElement element = XElement.Parse(body);

        // Act
        VaultLifeCycleState result = VaultLifeCycleState.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
