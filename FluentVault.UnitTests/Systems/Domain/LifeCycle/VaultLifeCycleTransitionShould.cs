
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures.LifeCycle;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.LifeCycle;
public class VaultLifeCycleTransitionShould
{
    private static readonly VaultLifeCycleTransitionFixture _fixture = new(string.Empty);

    [Fact]
    public void ParseVaultLifeCycleTransitionFromXElement()
    {
        // Arrange
        VaultLifeCycleTransition expectation = _fixture.Create();
        XElement element = _fixture.ParseXElement(expectation);

        // Act
        VaultLifeCycleTransition result = VaultLifeCycleTransition.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
