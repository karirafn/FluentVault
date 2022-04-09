using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.Property;

public class VaultPropertyShould
{
    [Fact]
    public void ParsePropertyDefinitionsFromXDocument()
    {
        // Arrange
        var (body, expectation) = VaultResponseFixtures.GetVaultPropertyFixtures(5);
        var document = XDocument.Parse(body);

        // Act
        var result = VaultProperty.ParseAll(document);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
