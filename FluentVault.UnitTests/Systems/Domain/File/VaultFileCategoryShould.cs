
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures.File;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultFileCategoryShould
{
    private static readonly VaultFileCategoryFixture _fixture = new(string.Empty);

    [Fact]
    public void ParseVaultFileCategoryFromXElement()
    {
        // Arrange
        VaultFileCategory expectation = _fixture.Create();
        XElement element =_fixture.ParseXElement(expectation);

        // Act
        VaultFileCategory result = VaultFileCategory.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
