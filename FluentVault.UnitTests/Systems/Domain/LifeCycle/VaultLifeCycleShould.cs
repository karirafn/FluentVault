using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures.LifeCycle;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.LifeCycle;

public class VaultLifecycleShould
{
    private static readonly VaultLifeCycleDefinitionFixture _fixture = new();

    [Fact]
    public void ParseLifeCyclesFromXDocument()
    {
        // Arrange
        int count = 5;
        IEnumerable<VaultLifeCycleDefinition> expectation = _fixture.CreateMany(count);
        XDocument document = _fixture.ParseXDocument(expectation);

        // Act
        IEnumerable<VaultLifeCycleDefinition> result = VaultLifeCycleDefinition.ParseAll(document);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
