
using System.Collections.Generic;
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
        (string body, IEnumerable<VaultUserInfo> expectation) = VaultResponseFixtures.GetVaultUserInfoFixture(5);
        XDocument element = XDocument.Parse(body);

        // Act
        IEnumerable<VaultUserInfo> result = VaultUserInfo.ParseAll(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
