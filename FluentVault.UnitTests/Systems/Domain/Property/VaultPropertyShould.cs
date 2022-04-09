using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures.Property;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.Property;

public class VaultPropertyShould
{
    private static readonly VaultPropertyFixture _fixture = new();


    [Fact]
    public void ParsePropertyDefinitionsFromXDocument()
    {
        // Arrange
        int count = 5;
        IEnumerable<VaultProperty> expectation = _fixture.CreateMany(count);
        XDocument document = _fixture.ParseXDocument(expectation);

        // Act
        IEnumerable<VaultProperty> result = VaultProperty.ParseAll(document);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
