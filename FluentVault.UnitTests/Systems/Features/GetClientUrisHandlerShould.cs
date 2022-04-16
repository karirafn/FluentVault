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

    private static readonly Expression<Func<IMediator, Task<VaultFile>>> GetLatestFileByMasterIdQuery
        = x => x.Send(It.IsAny<GetLatestFileByMasterIdQuery>(), It.IsAny<CancellationToken>());

    private static readonly Expression<Func<IMediator, Task<IEnumerable<VaultFolder>>>> GetFoldersByFileMasterIdsQuery
        = x => x.Send(It.IsAny<GetFoldersByFileMasterIdsQuery>(), It.IsAny<CancellationToken>());

    private static readonly Expression<Func<IMediator, Task<VaultItem>>> GetLatestItemByItemMasterIdQuery
        = x => x.Send(It.IsAny<GetLatestItemByItemMasterIdQuery>(), It.IsAny<CancellationToken>());

    [Fact]
    public async Task FindFile_WhenItExists()
    {
        // Arrange
        IOptions<VaultOptions> options = Options.Create(_fixture.Create<VaultOptions>());
        Mock<IMediator> mediator = new();

        mediator.Setup(GetLatestFileByMasterIdQuery)
            .ReturnsAsync(_fixture.Create<VaultFile>());

        mediator.Setup(GetFoldersByFileMasterIdsQuery)
            .ReturnsAsync(new[] { _fixture.Create<VaultFolder>() });

        GetClientUrisQuery query = _fixture.Create<GetClientUrisQuery>();
        GetClientUrisHandler sut = new(mediator.Object, options);

        // Act
        _ = await sut.Handle(query, CancellationToken.None);

        // Assert
        mediator.Verify(GetLatestFileByMasterIdQuery, Times.Once);
        mediator.Verify(GetFoldersByFileMasterIdsQuery, Times.Once);
        mediator.Verify(GetLatestItemByItemMasterIdQuery, Times.Never);
    }

    [Fact]
    public async Task FindItem_WhenItExists()
    {
        // Arrange
        IOptions<VaultOptions> options = Options.Create(_fixture.Create<VaultOptions>());
        Mock<IMediator> mediator = new();

        mediator.Setup(GetLatestFileByMasterIdQuery)
            .ThrowsAsync(new Exception());

        mediator.Setup(GetLatestItemByItemMasterIdQuery)
            .ReturnsAsync(_fixture.Create<VaultItem>());

        GetClientUrisQuery query = _fixture.Create<GetClientUrisQuery>();
        GetClientUrisHandler sut = new(mediator.Object, options);

        // Act
        _ = await sut.Handle(query, CancellationToken.None);

        // Assert
        mediator.Verify(GetLatestFileByMasterIdQuery, Times.Once);
        mediator.Verify(GetFoldersByFileMasterIdsQuery, Times.Never);
        mediator.Verify(GetLatestItemByItemMasterIdQuery, Times.Once);
    }

    [Fact]
    public async Task ThrowArgumentException_WhenNoEntityIsFound()
    {
        // Arrange
        IOptions<VaultOptions> options = Options.Create(_fixture.Create<VaultOptions>());
        Mock<IMediator> mediator = new();

        mediator.Setup(GetLatestFileByMasterIdQuery)
            .ThrowsAsync(new Exception());

        mediator.Setup(GetLatestItemByItemMasterIdQuery)
            .ThrowsAsync(new Exception());

        GetClientUrisQuery query = _fixture.Create<GetClientUrisQuery>();
        GetClientUrisHandler sut = new(mediator.Object, options);

        // Act
        Func<Task<(Uri VaultClient, Uri ThinClient)>> handle = () => sut.Handle(query, CancellationToken.None);

        // Assert
        await handle.Should().ThrowExactlyAsync<ArgumentException>();
        mediator.Verify(GetLatestFileByMasterIdQuery, Times.Once);
        mediator.Verify(GetFoldersByFileMasterIdsQuery, Times.Never);
        mediator.Verify(GetLatestItemByItemMasterIdQuery, Times.Once);
    }
}
