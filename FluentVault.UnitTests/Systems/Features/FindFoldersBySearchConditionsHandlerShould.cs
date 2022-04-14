using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using AutoFixture;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Features;
using FluentVault.TestFixtures;
using FluentVault.UnitTests.Helpers;

using Moq;

using Xunit;

namespace FluentVault.UnitTests.Systems.Features;
public class FindFoldersBySearchConditionsHandlerShould
{
    private static readonly SmartEnumFixture _fixture = new();
    private static readonly FindFoldersBySearchConditionsSerializer _serializer = new();

    [Fact]
    public async Task CallVaultService()
    {
        // Arrange
        VaultSearchFoldersResponse expectation = _fixture.Create<VaultSearchFoldersResponse>();
        XDocument response = _serializer.Serialize(expectation);

        Mock<IVaultService> vaultService = new();

        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        FindFoldersBySearchConditionsQuery query = _fixture.Create<FindFoldersBySearchConditionsQuery>();
        FindFoldersBySearchConditionsHandler sut = new(vaultService.Object);

        // Act
        VaultSearchFoldersResponse result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectation);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once());
    }
}
