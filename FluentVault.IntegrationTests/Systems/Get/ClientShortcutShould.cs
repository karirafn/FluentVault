using System;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Get;
public class ClientShortcutShould
{
    private static readonly VaultTestData _testData = new();

    [Fact]
    public async Task ReturnThickClientUri()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        Uri result = await sut.Get.ClientShortcut
            .WithEntityClass(VaultEntityClass.File)
            .WithClientType(VaultClientType.Thick)
            .WithMasterId(_testData.TestPartMasterId)
            .ExecuteAsync(CancellationToken.None);

        // Assert
        result.ToString().Should().Contain(_testData.TestPartFilename);
    }

    [Fact]
    public async Task ReturnThinClientUri()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        Uri result = await sut.Get.ClientShortcut
            .WithEntityClass(VaultEntityClass.File)
            .WithClientType(VaultClientType.Thin)
            .WithMasterId(_testData.TestPartMasterId)
            .ExecuteAsync(CancellationToken.None);

        // Assert
        result.ToString().Should().EndWith(_testData.TestPartMasterId.ToString());
    }
}
