using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Domain.Search;
using FluentVault.Features;
using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Features;
public class SearchFilesHandlerShould : IAsyncLifetime
{
    private static readonly VaultTestData _testData = new();
    private static readonly IEnumerable<IDictionary<string, string>> _sortConditions = new List<SortCondition>().Select(x => x.Attributes);
    private static readonly VaultFolderId[] _folderIds = Array.Empty<VaultFolderId>();
    private static readonly bool _recurseFolders = true;
    private static readonly bool _latestOnly = true;
    private static readonly string _bookmark = string.Empty;

    private readonly IVaultRequestService _service;
    private readonly VaultOptions _options;
    private readonly SearchFilesHandler _sut;

    private VaultSessionCredentials _session;

    public SearchFilesHandlerShould()
    {
        _service = new VaultRequestServiceFixture().VaultRequestService;
        _sut = new(_service);
        _options = new VaultOptionsFixture().Options;
        _session = new();
    }

    public async Task InitializeAsync() => _session = await new SignInHandler(_service).Handle(new SignInCommand(_options), default);
    async Task IAsyncLifetime.DisposeAsync() => await new SignOutHandler(_service).Handle(new SignOutCommand(_session ?? new()), default);

    [Fact]
    public async Task FindFiles()
    {
        // Arrange
        var searchConditions = new SearchCondition[]
        {
            new(StringSearchProperty.FileExtension.Value, SearchOperator.IsEqualTo, "ipt", SearchPropertyType.SingleProperty, SearchRule.Must)
        }.Select(x => x.Attributes);

        
        SearchFilesCommand command = new(searchConditions, _sortConditions, _folderIds, _recurseFolders, _latestOnly, _bookmark, _session);

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
        SearchFilesCommand command = new(searchConditions, _sortConditions, _folderIds, _recurseFolders, latestOnly, _bookmark, _session);

        // Act
        FileSearchResult results = await _sut.Handle(command, default);

        // Assert
        results.Should().NotBeNull();
        results.Files.Should().NotBeEmpty();
    }
}
