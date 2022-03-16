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

namespace FluentVault.IntegrationTests.Systems;
public class SearchFilesHandlerShould
{
    [Fact]
    public async Task FindFiles()
    {
        // Arrange
        IVaultRequestService service = new VaultRequestServiceFixture().VaultRequestService;
        VaultOptions options = new VaultOptionsFixture().Options;
        VaultSessionCredentials session = await new SignInHandler(service).Handle(new SignInCommand(options), default);
        SearchFilesHandler sut = new(service);

        var searchConditions = new SearchCondition[]
        {
            new(StringSearchProperty.FileExtension.Value, SearchOperator.IsEqualTo, "ipt", SearchPropertyType.SingleProperty, SearchRule.Must),
            new(StringSearchProperty.State.Value, SearchOperator.IsEqualTo, "Invalid", SearchPropertyType.SingleProperty, SearchRule.Must)
        }.Select(x => x.Attributes);

        var sortConditions = new List<SortCondition>().Select(x => x.Attributes);
        var folderIds = Array.Empty<VaultFolderId>();
        var recurseFolders = true;
        var latestOnly = true;
        var bookmark = string.Empty;
        SearchFilesCommand command = new(searchConditions, sortConditions, folderIds, recurseFolders, latestOnly, bookmark, session);

        // Act
        var results = await sut.Handle(command, default);

        // Assert
        results.Should().NotBeNull();
        results.Files.Should().NotBeEmpty();
    }
}
