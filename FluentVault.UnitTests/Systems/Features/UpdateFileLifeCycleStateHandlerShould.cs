﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using AutoFixture;

using FluentVault.Common;
using FluentVault.Domain.Search.Files;
using FluentVault.Features;
using FluentVault.TestFixtures;
using FluentVault.UnitTests.Helpers;

using MediatR;

using Moq;

using Xunit;

namespace FluentVault.UnitTests.Systems.Features;
public class UpdateFileLifeCycleStateHandlerShould
{
    private static readonly SmartEnumFixture _fixture = new();

    [Fact]
    public async Task CallVaultService()
    {
        // Arrange
        IEnumerable<VaultFile> files = _fixture.CreateMany<VaultFile>();
        Mock<IMediator> mediator = new();
        Mock<IVaultService> vaultService = new();

        UpdateFileLifeCycleStatesCommand command = new(
            Enumerable.Empty<string>(),
            _fixture.CreateMany<VaultMasterId>(),
            _fixture.CreateMany<VaultLifeCycleStateId>(),
            _fixture.Create<string>());
        UpdateFileLifeCycleStatesHandler sut = new(mediator.Object, vaultService.Object);

        XDocument response = sut.Serializer.Serialize(files);
        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        // Act
        IEnumerable<VaultFile> result = await sut.Handle(command, CancellationToken.None);

        // Assert
        mediator.Verify(x => x.Send(It.IsAny<FindFilesBySearchConditionsQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once);
    }

    [Fact]
    public async Task CallMediator_WhenFilenamesIsNotEmpty()
    {
        // Arrange
        IEnumerable<VaultFile> files = _fixture.CreateMany<VaultFile>();
        VaultSearchFilesResponse mediatorResponse = _fixture.Create<VaultSearchFilesResponse>();
        Mock<IMediator> mediator = new();
        Mock<IVaultService> vaultService = new();

        UpdateFileLifeCycleStatesCommand command = _fixture.Create<UpdateFileLifeCycleStatesCommand>();
        UpdateFileLifeCycleStatesHandler sut = new(mediator.Object, vaultService.Object);

        XDocument vaultResponse = sut.Serializer.Serialize(files);
        mediator.Setup(x => x.Send(
                It.IsAny<FindFilesBySearchConditionsQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(mediatorResponse);

        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(vaultResponse);

        // Act
        IEnumerable<VaultFile> result = await sut.Handle(command, CancellationToken.None);

        // Assert
        mediator.Verify(x => x.Send(It.IsAny<FindFilesBySearchConditionsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once);
    }
}
