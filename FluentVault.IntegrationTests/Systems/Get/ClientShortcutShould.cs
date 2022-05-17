using System;
using System.Collections.Generic;
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
    public async Task ReturnAllCategoryConfigurations()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<Uri> result = await sut.Get.ClientShortcut
            .WithEntityClass(VaultEntityClass.File)
            .WithClientType(VaultClientType.Thick)
            .WithMasterId(_testData.TestPartMasterId)
            .ExecuteAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
    }
}
