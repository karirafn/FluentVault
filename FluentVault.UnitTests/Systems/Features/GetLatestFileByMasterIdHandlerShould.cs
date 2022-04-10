using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Features;
using FluentVault.TestFixtures.File;

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
        VaultFile file = _fixture.Create();
        XDocument response = _fixture.ParseXDocument(file);

        Mock<IVaultService> vaultService = new();

        vaultService.Setup(x => x.SendAsync(
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<Action<XElement, XNamespace>?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        GetLatestFileByMasterIdQuery query = new(file.MasterId);
        GetLatestFileByMasterIdHandler sut = new(vaultService.Object);

        // Act
        VaultFile result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(file);
        vaultService.Verify(x => x.SendAsync(
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<Action<XElement, XNamespace>?>(),
                It.IsAny<CancellationToken>()),
            Times.Once());
    }
}
