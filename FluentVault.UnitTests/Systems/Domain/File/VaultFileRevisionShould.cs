
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures.File;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultFilerRevisionShould
{
    private static readonly VaultFileRevisionFixture _fixture = new(string.Empty);

    [Fact]
    public void ParseVaultFileRevisionFromXElement()
    {
        // Arrange
        VaultFileRevision expectation = _fixture.Create();
        XElement element = _fixture.ParseXElement(expectation);

        // Act
        VaultFileRevision result = VaultFileRevision.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
