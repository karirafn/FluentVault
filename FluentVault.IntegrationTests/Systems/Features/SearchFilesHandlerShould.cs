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
    private readonly IVaultRequestService _service;
    private readonly VaultOptions _options;
    private readonly SearchFilesHandler _sut;
    private VaultSessionCredentials? _session;

    public SearchFilesHandlerShould()
    {
        _service = new VaultRequestServiceFixture().VaultRequestService;
        _sut = new(_service);
        _options = new VaultOptionsFixture().Options;
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

        IEnumerable<IDictionary<string, string>> sortConditions = new List<SortCondition>().Select(x => x.Attributes);
        VaultFolderId[] folderIds = Array.Empty<VaultFolderId>();
        bool recurseFolders = true;
        bool latestOnly = true;
        string bookmark = string.Empty;
        SearchFilesCommand command = new(searchConditions, sortConditions, folderIds, recurseFolders, latestOnly, bookmark, _session ?? new());

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

        IEnumerable<IDictionary<string, string>> sortConditions = new List<SortCondition>().Select(x => x.Attributes);
        VaultFolderId[] folderIds = Array.Empty<VaultFolderId>();
        bool recurseFolders = true;
        bool latestOnly = false;
        string bookmark = string.Empty;
        SearchFilesCommand command = new(searchConditions, sortConditions, folderIds, recurseFolders, latestOnly, bookmark, _session ?? new());

        // Act
        FileSearchResult results = await _sut.Handle(command, default);

        // Assert
        results.Should().NotBeNull();
        results.Files.Should().NotBeEmpty();
    }
}
