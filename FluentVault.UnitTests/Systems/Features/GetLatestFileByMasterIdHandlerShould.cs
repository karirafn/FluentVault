using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Features;
using FluentVault.TestFixtures.File;
using FluentVault.UnitTests.Helpers;

using Moq;

using Xunit;

namespace FluentVault.UnitTests.Systems.Features;
public class GetLatestFileByMasterIdHandlerShould
{
    private static readonly VaultFileFixture _fixture = new();

    [Fact]
    public async Task CallVaultService()
    {
        // Arrange
        VaultFile expectation = _fixture.Create();
        XDocument response = _fixture.ParseXDocument(expectation);

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
