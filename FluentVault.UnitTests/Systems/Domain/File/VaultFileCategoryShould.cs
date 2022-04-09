
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultFileCategoryShould
{
    [Fact]
    public void ParseVaultFileCategoryFromXElement()
    {
        // Arrange
        (string body, VaultFileCategory expectation) = VaultResponseFixtures.GetVaultFileCategoryFixture();
        XElement element = XElement.Parse(body);

        // Act
        VaultFileCategory result = VaultFileCategory.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
