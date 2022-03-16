
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultUserInfoShould
{
    [Fact]
    public void ParseVaultUserInfoFromXDocument()
    {
        // Arrange
        var (body, expectation) = VaultResponseFixtures.GetVaultUserInfoFixture(5);
        var element = XDocument.Parse(body);

        // Act
        var result = VaultUserInfo.ParseAll(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
