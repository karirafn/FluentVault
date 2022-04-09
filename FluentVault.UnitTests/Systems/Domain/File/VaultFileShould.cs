using System;
using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;
using FluentAssertions.Extensions;

using FluentVault.TestFixtures.File;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.File;
public class VaultFileShould
{
    private static readonly VaultFileFixture _fixture = new();

    [Fact]
    public void ParseSingleFileFromXDocument()
    {
        // Arrange
        VaultFile expectation = _fixture.Create();
        XDocument document = _fixture.ParseXDocument(expectation);

        // Act
        VaultFile result = VaultFile.ParseSingle(document);

        // Assert
        result.Should().BeEquivalentTo(expectation, options => options
                .Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, 1.Seconds()))
                .WhenTypeIs<DateTime>());
    }

    [Fact]
    public void ParseAllFilesFromXDocument()
    {
        // Arrange
        int fileCount = 2;
        IEnumerable<VaultFile> expectation = _fixture.CreateMany(fileCount);
        XDocument document = _fixture.ParseXDocument(expectation);

        // Act
        IEnumerable<VaultFile> result = VaultFile.ParseAll(document);

        // Assert
        result.Should().HaveCount(fileCount);
        result.Should().BeEquivalentTo(expectation, options => options
                .Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, 1.Seconds()))
                .WhenTypeIs<DateTime>());
    }
}
