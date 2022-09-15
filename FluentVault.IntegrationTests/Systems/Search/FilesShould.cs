﻿using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Search;
public class FilesShould
{
    private static readonly VaultTestData _testData = new();

    // This test will fail if the Vault has no part files
    [Fact]
    public async Task FindFiles()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<VaultFile> result = await sut.Search.Files
            .BySystemProperty(VaultSystemProperty.FileExtension)
            .Containing("ipt")
            .GetPagedResultAsync();

        // Assert
        result.Should().NotBeNullOrEmpty();
    }

    // This test will fail if test part has fewer than 2 versions
    [Fact]
    public async Task FindMultipleVersions_WhenCallingCetAllVersions()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<VaultFile> result = await sut.Search.Files
            .BySystemProperty(VaultSystemProperty.FileName)
            .EqualTo(_testData.TestPartFilename)
            .AllVersions
            .GetPagedResultAsync();

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCountGreaterThan(1);
        result.Should().AllSatisfy(file => file.MasterId.Should().Be(_testData.TestPartMasterId));
    }

    [Fact]
    public async Task FindSingleFile()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        VaultFile? result = await sut.Search.Files
            .BySystemProperty(VaultSystemProperty.FileName)
            .Containing(_testData.TestPartFilename)
            .GetFirstResultAsync();

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task ReturnEmptyWhenNothingIsFound()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<VaultFile> result = await sut.Search.Files
            .BySystemProperty(VaultSystemProperty.FileName)
            .Containing("this-definitely-does-not-exist")
            .GetAllResultsAsync();

        // Assert
        result.Should().BeEmpty();
    }
}
