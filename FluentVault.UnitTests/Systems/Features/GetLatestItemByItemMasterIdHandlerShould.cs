using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using AutoFixture;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Features;
using FluentVault.TestFixtures;
using FluentVault.UnitTests.Helpers;

using Moq;

using Xunit;

namespace FluentVault.UnitTests.Systems.Features;
public class GetLatestItemByItemMasterIdHandlerShould
{
    private static readonly SmartEnumFixture _fixture = new();
    private static readonly GetLatestItemByItemMasterIdSerializer _serializer = new();

    [Fact]
    public async Task CallVaultService()
    {
        // Arrange
        VaultItem expectation = _fixture.Create<VaultItem>();
        XDocument response = _serializer.Serialize(expectation);

        Mock<IVaultService> vaultService = new();

        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        GetLatestItemByItemMasterIdQuery query = new(expectation.MasterId);
        GetLatestItemByItemMasterIdHandler sut = new(vaultService.Object);

        // Act
        VaultItem result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectation);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once());
    }
}
