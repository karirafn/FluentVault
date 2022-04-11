using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using AutoFixture;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Domain.Search;
using FluentVault.Features;
using FluentVault.TestFixtures.Search;
using FluentVault.UnitTests.Helpers;

using Moq;

using Xunit;

namespace FluentVault.UnitTests.Systems.Features;
public class SearchFilesHandlerShould
{
    private static readonly Fixture _fixture = new();
    private static readonly VaultFileSearchResultFixture _fileFixture = new();

    [Fact]
    public async Task CallVaultService()
    {
        // Arrange
        VaultFileSearchResult expectation = _fileFixture.Create();
        XDocument response = _fileFixture.ParseXDocument(expectation);

        Mock<IVaultService> vaultService = new();

        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        SearchFilesCommand query = _fixture.Create<SearchFilesCommand>();
        SearchFilesHandler sut = new(vaultService.Object);

        // Act
        VaultFileSearchResult result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectation);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once());
    }
}
