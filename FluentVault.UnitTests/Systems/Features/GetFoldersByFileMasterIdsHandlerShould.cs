using System.Collections.Generic;
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
public class GetFoldersByFileMasterIdsHandlerShould
{
    private static readonly SmartEnumFixture _fixture = new();
    private static readonly GetFoldersByFileMasterIdsSerializer _serializer = new();

    [Fact]
    public async Task CallVaultService()
    {
        // Arrange
        IEnumerable<VaultFolder> expectation = _fixture.CreateMany<VaultFolder>();
        XDocument response = _serializer.Serialize(expectation);
        Mock<IVaultService> vaultService = new();

        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        GetFoldersByFileMasterIdsQuery query = _fixture.Create<GetFoldersByFileMasterIdsQuery>();
        GetFoldersByFileMasterIdsHandler sut = new(vaultService.Object);

        // Act
        IEnumerable<VaultFolder> result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectation);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once());
    }
}
