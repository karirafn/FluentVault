using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.Domain.Search;
using FluentVault.Features;
using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Features;
public class SearchFilesHandlerShould : IClassFixture<VaultFixture>
{
    private static readonly VaultTestData _testData = new();
    private static readonly IEnumerable<IDictionary<string, object>> _sortConditions = new List<SortCondition>().Select(x => x.Attributes);
    private static readonly VaultFolderId[] _folderIds = Array.Empty<VaultFolderId>();
    private static readonly bool _recurseFolders = true;
    private static readonly bool _latestOnly = true;
    private static readonly string _bookmark = string.Empty;
    private readonly SearchFilesHandler _sut;

    public SearchFilesHandlerShould(VaultFixture fixture)
    {
        _sut = new SearchFilesHandler(fixture.Service);
    }

    [Fact]
    public async Task FindFiles()
    {
        // Arrange
        var searchConditions = new SearchCondition[]
        {
            new(StringSearchProperty.FileExtension.Value, SearchOperator.IsEqualTo, "ipt", SearchPropertyType.SingleProperty, SearchRule.Must)
        }.Select(x => x.Attributes);

        
        SearchFilesCommand command = new(searchConditions, _sortConditions, _folderIds, _recurseFolders, _latestOnly, _bookmark);

        // Act
        var results = await _sut.Handle(command, default);

        // Assert
        results.Should().NotBeNull();
        results.Files.Should().NotBeEmpty();
    }

    [Fact]
    public async Task FindMultipleVersions_WhenLatestOnlyIsFalse()
    {
        // Arrange
        var searchConditions = new SearchCondition[]
        {
            new(StringSearchProperty.FileName.Value, SearchOperator.IsEqualTo, _testData.TestPartFilename, SearchPropertyType.SingleProperty, SearchRule.Must),
        }.Select(x => x.Attributes);

        var latestOnly = false;
        SearchFilesCommand command = new(searchConditions, _sortConditions, _folderIds, _recurseFolders, latestOnly, _bookmark);

        // Act
        FileSearchResult results = await _sut.Handle(command, default);

        // Assert
        results.Should().NotBeNull();
        results.Files.Should().NotBeEmpty();
    }
}
