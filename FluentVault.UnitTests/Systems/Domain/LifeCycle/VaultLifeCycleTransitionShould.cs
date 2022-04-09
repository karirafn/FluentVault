
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultLifeCycleTransitionShould
{
    [Fact]
    public void ParseVaultLifeCycleTransitionFromXElement()
    {
        // Arrange
        (string body, VaultLifeCycleTransition expectation) = VaultResponseFixtures.GetVaultLifeCycleTransitionFixture();
        XElement element = XElement.Parse(body);

        // Act
        VaultLifeCycleTransition result = VaultLifeCycleTransition.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
