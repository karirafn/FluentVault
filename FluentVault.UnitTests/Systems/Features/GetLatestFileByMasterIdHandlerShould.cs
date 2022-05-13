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
public class GetLatestFileByMasterIdHandlerShould
{
    private static readonly SmartEnumFixture _fixture = new();

    [Fact]
    public async Task CallVaultService()
    {
        // Arrange
        VaultFile expectation = _fixture.Create<VaultFile>();
        Mock<IMediator> mediator = new();
        Mock<IVaultService> vaultService = new();

        GetLatestFileByMasterIdQuery query = new(expectation.MasterId);
        GetLatestFileByMasterIdHandler sut = new(mediator.Object, vaultService.Object);

        XDocument response = sut.Serializer.Serialize(expectation);
        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        // Act
        VaultFile result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectation);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once());
    }
}
