
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultFilerRevisionShould
{
    [Fact]
    public void ParseVaultFileRevisionFromXElement()
    {
        // Arrange
        (string body, VaultFileRevision expectation) = VaultResponseFixtures.GetVaultFileRevisionFixture();
        XElement element = XElement.Parse(body);

        // Act
        VaultFileRevision result = VaultFileRevision.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
