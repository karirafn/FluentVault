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
        IEnumerable<VaultPropertyInstance> result = await sut.Get.Properties
            .ForEntityClass(VaultEntityClass.File)
            .WithId(_testData.TestPartIterationWithoutDrawingId)
            .WithProperty(VaultSystemProperty.FileName)
            .ExecuteAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        result.First().Value.Should().Be(_testData.TestPartFilename);
    }

    [Fact]
    public async Task ReturnEmptyStringValue_WhenPropertyHasNoValue()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<VaultPropertyInstance> result = await sut.Get.Properties
            .ForEntityClass(VaultEntityClass.File)
            .WithId(_testData.TestPartIterationWithoutDrawingId)
            .WithProperty(VaultSystemProperty.Revision)
            .ExecuteAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        result.First().Value.Should().BeEmpty();
    }
}
