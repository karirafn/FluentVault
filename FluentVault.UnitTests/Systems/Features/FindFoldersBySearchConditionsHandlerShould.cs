using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using AutoFixture;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Domain.Search.Folders;
using FluentVault.Features;
using FluentVault.TestFixtures;
using FluentVault.UnitTests.Helpers;

using MediatR;

using Moq;

using Xunit;

namespace FluentVault.UnitTests.Systems.Features;
public class FindFoldersBySearchConditionsHandlerShould
{
    private static readonly SmartEnumFixture _fixture = new();

    [Fact]
    public async Task CallVaultService()
    {
        // Arrange
        Mock<IVaultService> vaultService = new();
        Mock<IMediator> mediator = new();

        FindFoldersBySearchConditionsQuery query = _fixture.Create<FindFoldersBySearchConditionsQuery>();
        FindFoldersBySearchConditionsHandler sut = new(mediator.Object, vaultService.Object);

        VaultSearchFoldersResponse expectation = _fixture.Create<VaultSearchFoldersResponse>();
        XDocument response = sut.Serializer.Serialize(expectation);
        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        // Act
        VaultSearchFoldersResponse result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectation);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once());
    }
}
