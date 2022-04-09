
using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures.User;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultUserInfoShould
{
    private static readonly VaultUserInfoFixture _fixture = new();

    [Fact]
    public void ParseVaultUserInfoFromXDocument()
    {
        // Arrange
        int count = 5;
        IEnumerable<VaultUserInfo> expectation = _fixture.CreateMany(count);
        XDocument document = _fixture.ParseXDocument(expectation);

        // Act
        IEnumerable<VaultUserInfo> result = VaultUserInfo.ParseAll(document);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
