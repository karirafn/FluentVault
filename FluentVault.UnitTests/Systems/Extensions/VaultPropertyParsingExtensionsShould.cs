using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Domain.Property;
using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions;

public class VaultPropertyParsingExtensionsShould
{
    [Fact]
    public void ReturnValidResult_WhenParsingValidString()
    {
        // Arrange
        var (body, expectation) = VaultResponseFixtures.GetVaultPropertyFixtures(5);
        var document = XDocument.Parse(body);

        // Act
        var result = document.ParseProperties();

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
