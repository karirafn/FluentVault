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
public class GetLatestFileByMasterIdHandlerShould
{
    private static readonly SmartEnumFixture _fixture = new();
    private static readonly GetLatestFileByMasterIdSerializer _serializer = new();

    [Fact]
    public async Task CallVaultService()
    {
        // Arrange
        VaultFile expectation = _fixture.Create<VaultFile>();
        XDocument response = _serializer.Serialize(expectation);

        Mock<IVaultService> vaultService = new();

        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        GetLatestFileByMasterIdQuery query = new(expectation.MasterId);
        GetLatestFileByMasterIdHandler sut = new(vaultService.Object);

        // Act
        VaultFile result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectation);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once());
    }
}
