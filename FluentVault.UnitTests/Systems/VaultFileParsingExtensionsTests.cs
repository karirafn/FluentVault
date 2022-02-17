using System;
using System.Linq;
using System.Xml.Linq;

using FluentAssertions;
using FluentAssertions.Extensions;

using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems;

public class VaultFileParsingExtensionsTests
{
    [Fact]
    public void ParseVaultFile_ShouldReturnFile_WhenParsingValidString()
    {
        // Arrange
        var (body, expectation) = BodyFixtures.GetVaultFileFixtures(1);
        var document = XDocument.Parse(body);

        // Act
        var result = document.ParseVaultFile();

        // Assert
        result.Should().BeEquivalentTo(expectation.Single(), options => options
                .Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, 1.Seconds()))
                .WhenTypeIs<DateTime>());
    }

    [Fact]
    public void ParseAllVaultFiles_ShouldReturnAllFiles_WhenParsingValidString()
    {
        // Arrange
        int fileCount = 3;
        var (body, expectation) = BodyFixtures.GetVaultFileFixtures(fileCount);
        var document = XDocument.Parse(body);

        // Act
        var result = document.ParseAllVaultFiles();

        // Assert
        result.Should().HaveCount(fileCount);
        result.Should().BeEquivalentTo(expectation, options => options
                .Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, 1.Seconds()))
                .WhenTypeIs<DateTime>());
    }
}
