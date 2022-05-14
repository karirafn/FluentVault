using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Get;
public class FilePropertiesShould
{
    private static readonly VaultTestData _testData = new();

    [Fact]
    public async Task ReturnEmptyArray_WhenUsingDefaultArguments()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<VaultPropertyInstance> result = await sut.Get.File.Properties
            .ByFileId(_testData.TestPartIterationWithoutDrawingId)
            .AndProperty(VaultSystemProperty.FileName)
            .ExecuteAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        result.First().Value.Should().Be(_testData.TestPartFilename);
    }
}
