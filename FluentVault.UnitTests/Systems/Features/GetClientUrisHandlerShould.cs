using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using AutoFixture;

using FluentAssertions;

using FluentVault.Features;

using MediatR;

using Microsoft.Extensions.Options;

using Moq;

using Xunit;

namespace FluentVault.UnitTests.Systems.Features;
public class GetClientUrisHandlerShould
{
    private static readonly TestFixtures.SmartEnumFixture _fixture = new();

    private static readonly Expression<Func<IMediator, Task<VaultFile>>> _getLatestFileByMasterIdQuery
        = x => x.Send(It.IsAny<GetLatestFileByMasterIdQuery>(), It.IsAny<CancellationToken>());

    private static readonly Expression<Func<IMediator, Task<IEnumerable<VaultFolder>>>> _getFoldersByFileMasterIdsQuery
        = x => x.Send(It.IsAny<GetFoldersByFileMasterIdsQuery>(), It.IsAny<CancellationToken>());

    private static readonly Expression<Func<IMediator, Task<VaultItem>>> _getLatestItemByItemMasterIdQuery
        = x => x.Send(It.IsAny<GetLatestItemByItemMasterIdQuery>(), It.IsAny<CancellationToken>());

    [Fact]
    public async Task FindFile_WhenItExists()
    {
        // Arrange
        IOptions<VaultOptions> options = Options.Create(_fixture.Create<VaultOptions>());
        Mock<IMediator> mediator = new();

        mediator.Setup(_getLatestFileByMasterIdQuery)
            .ReturnsAsync(_fixture.Create<VaultFile>());

        mediator.Setup(_getFoldersByFileMasterIdsQuery)
            .ReturnsAsync(new[] { _fixture.Create<VaultFolder>() });

        GetClientUrisQuery query = _fixture.Create<GetClientUrisQuery>();
        GetClientUrisHandler sut = new(mediator.Object, options);

        // Act
        _ = await sut.Handle(query, CancellationToken.None);

        // Assert
        mediator.Verify(_getLatestFileByMasterIdQuery, Times.Once);
        mediator.Verify(_getFoldersByFileMasterIdsQuery, Times.Once);
        mediator.Verify(_getLatestItemByItemMasterIdQuery, Times.Never);
    }

    [Fact]
    public async Task FindItem_WhenItExists()
    {
        // Arrange
        IOptions<VaultOptions> options = Options.Create(_fixture.Create<VaultOptions>());
        Mock<IMediator> mediator = new();

        mediator.Setup(_getLatestFileByMasterIdQuery)
            .ThrowsAsync(new Exception());

        mediator.Setup(_getLatestItemByItemMasterIdQuery)
            .ReturnsAsync(_fixture.Create<VaultItem>());

        GetClientUrisQuery query = _fixture.Create<GetClientUrisQuery>();
        GetClientUrisHandler sut = new(mediator.Object, options);

        // Act
        _ = await sut.Handle(query, CancellationToken.None);

        // Assert
        mediator.Verify(_getLatestFileByMasterIdQuery, Times.Once);
        mediator.Verify(_getFoldersByFileMasterIdsQuery, Times.Never);
        mediator.Verify(_getLatestItemByItemMasterIdQuery, Times.Once);
    }

    [Fact]
    public async Task ThrowArgumentException_WhenNoEntityIsFound()
    {
        // Arrange
        IOptions<VaultOptions> options = Options.Create(_fixture.Create<VaultOptions>());
        Mock<IMediator> mediator = new();

        mediator.Setup(_getLatestFileByMasterIdQuery)
            .ThrowsAsync(new Exception());

        mediator.Setup(_getLatestItemByItemMasterIdQuery)
            .ThrowsAsync(new Exception());

        GetClientUrisQuery query = _fixture.Create<GetClientUrisQuery>();
        GetClientUrisHandler sut = new(mediator.Object, options);

        // Act
        Func<Task<(Uri VaultClient, Uri ThinClient)>> handle = () => sut.Handle(query, CancellationToken.None);

        // Assert
        await handle.Should().ThrowExactlyAsync<ArgumentException>();
        mediator.Verify(_getLatestFileByMasterIdQuery, Times.Once);
        mediator.Verify(_getFoldersByFileMasterIdsQuery, Times.Never);
        mediator.Verify(_getLatestItemByItemMasterIdQuery, Times.Once);
    }
}
