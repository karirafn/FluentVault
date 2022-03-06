using System.Xml.Linq;

using FluentAssertions;

using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.Property;

public class VaultPropertyDefinitionShould
{
    [Fact]
    public void ParsePropertyDefinitionsFromXDocument()
    {
        // Arrange
        var (body, expectation) = VaultResponseFixtures.GetVaultPropertyFixtures(5);
        var document = XDocument.Parse(body);

        // Act
        var result = VaultPropertyDefinition.ParseAll(document);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
