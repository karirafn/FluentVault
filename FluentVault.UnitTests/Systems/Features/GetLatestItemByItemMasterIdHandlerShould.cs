using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using AutoFixture;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Features;
using FluentVault.TestFixtures;
using FluentVault.UnitTests.Helpers;

using MediatR;

using Moq;

using Xunit;

namespace FluentVault.UnitTests.Systems.Features;
public class GetLatestItemByItemMasterIdHandlerShould
{
    private static readonly SmartEnumFixture _fixture = new();

    [Fact]
    public async Task CallVaultService()
    {
        // Arrange
        VaultItem expectation = _fixture.Create<VaultItem>();
        Mock<IMediator> mediator = new();
        Mock<IVaultService> vaultService = new();

        GetLatestItemByItemMasterIdQuery query = new(expectation.MasterId);
        GetLatestItemByItemMasterIdHandler sut = new(mediator.Object, vaultService.Object);

        XDocument response = sut.Serializer.Serialize(expectation);
        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        // Act
        VaultItem result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectation);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once());
    }
}
