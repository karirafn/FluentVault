
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures.File;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultFileLifeCycleShould
{
    private static readonly VaultFileLifeCycleFixture _fixture = new(string.Empty);

    [Fact]
    public void ParseVaultFileLifeCycleFromXElement()
    {
        // Arrange
        VaultFileLifeCycle expectation = _fixture.Create();
        XElement element = _fixture.ParseXElement(expectation);

        // Act
        VaultFileLifeCycle result = VaultFileLifeCycle.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
