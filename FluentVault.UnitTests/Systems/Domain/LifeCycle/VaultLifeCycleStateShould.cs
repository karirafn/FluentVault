
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures.LifeCycle;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.LifeCycle;
public class VaultLifeCycleStateShould
{
    private static readonly VaultLifeCycleStateFixture _fixture = new(string.Empty);
    [Fact]
    public void Parse()
    {
        // Arrange
        VaultLifeCycleState expectation = _fixture.Create();
        XElement element = _fixture.ParseXElement(expectation);

        // Act
        VaultLifeCycleState result = VaultLifeCycleState.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
