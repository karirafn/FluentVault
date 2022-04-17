using System.Collections.Generic;
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
public class UpdateFilePropertyDefinitionsHandlerShould
{
    private static readonly SmartEnumFixture _fixture = new();

    [Fact]
    public async Task CallVaultService()
    {
        // Arrange
        IEnumerable<VaultFile> files = _fixture.CreateMany<VaultFile>();
        Mock<IMediator> mediator = new();
        Mock<IVaultService> vaultService = new();

        UpdateFilePropertyDefinitionsCommand command = new(
            MasterIds: _fixture.Create<List<VaultMasterId>>(),
            AddedPropertyIds: _fixture.Create<List<VaultPropertyDefinitionId>>(),
            RemovedPropertyIds: _fixture.Create<List<VaultPropertyDefinitionId>>());
        UpdateFilePropertyDefinitionsHandler sut = new(mediator.Object, vaultService.Object);

        XDocument vaultServiceResponse = sut.Serializer.Serialize(files);
        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(vaultServiceResponse);

        // Act
        IEnumerable<VaultFile> result = await sut.Handle(command, CancellationToken.None);

        // Assert
        mediator.Verify(x => x.Send(It.IsAny<FindFilesBySearchConditionsQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        mediator.Verify(x => x.Send(It.IsAny<GetAllPropertyDefinitionInfosQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once);
    }

    [Fact]
    public async Task SearchFiles_WhenFilenamesIsNotEmpty()
    {
        // Arrange
        IEnumerable<VaultFile> files = _fixture.CreateMany<VaultFile>();
        Mock<IMediator> mediator = new();
        Mock<IVaultService> vaultService = new();

        UpdateFilePropertyDefinitionsCommand command = new(
            MasterIds: new List<VaultMasterId>(),
            AddedPropertyIds: _fixture.Create<List<VaultPropertyDefinitionId>>(),
            RemovedPropertyIds: _fixture.Create<List<VaultPropertyDefinitionId>>(),
            Filenames: _fixture.CreateMany<string>());
        UpdateFilePropertyDefinitionsHandler sut = new(mediator.Object, vaultService.Object);
        XDocument vaultServiceResponse = sut.Serializer.Serialize(files);

        mediator.Setup(x => x.Send(It.IsAny<FindFilesBySearchConditionsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_fixture.Create<VaultSearchFilesResponse>());

        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(vaultServiceResponse);

        // Act
        IEnumerable<VaultFile> result = await sut.Handle(command, CancellationToken.None);

        // Assert
        mediator.Verify(x => x.Send(It.IsAny<FindFilesBySearchConditionsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        mediator.Verify(x => x.Send(It.IsAny<GetAllPropertyDefinitionInfosQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once);
    }

    [Fact]
    public async Task GetPropertiesOnce_WhenAddedPropertyNamesIsNotEmpty()
    {
        // Arrange
        IEnumerable<VaultFile> files = _fixture.CreateMany<VaultFile>();
        Mock<IMediator> mediator = new();
        Mock<IVaultService> vaultService = new();

        UpdateFilePropertyDefinitionsCommand command = new(
            MasterIds: _fixture.Create<List<VaultMasterId>>(),
            AddedPropertyIds: new List<VaultPropertyDefinitionId>(),
            RemovedPropertyIds: new List<VaultPropertyDefinitionId>(),
            Filenames: null,
            AddedPropertyNames: _fixture.CreateMany<string>());
        UpdateFilePropertyDefinitionsHandler sut = new(mediator.Object, vaultService.Object);

        XDocument vaultServiceResponse = sut.Serializer.Serialize(files);
        mediator.Setup(x => x.Send(It.IsAny<GetAllPropertyDefinitionInfosQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_fixture.CreateMany<VaultProperty>());

        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(vaultServiceResponse);

        // Act
        IEnumerable<VaultFile> result = await sut.Handle(command, CancellationToken.None);

        // Assert
        mediator.Verify(x => x.Send(It.IsAny<FindFilesBySearchConditionsQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        mediator.Verify(x => x.Send(It.IsAny<GetAllPropertyDefinitionInfosQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once);
    }

    [Fact]
    public async Task GetPropertiesOnce_WhenAddedAndRemovedPropertyNamesIsNotEmpty()
    {
        // Arrange
        IEnumerable<VaultFile> files = _fixture.CreateMany<VaultFile>();
        Mock<IMediator> mediator = new();
        Mock<IVaultService> vaultService = new();

        UpdateFilePropertyDefinitionsCommand command = new(
            MasterIds: _fixture.Create<List<VaultMasterId>>(),
            AddedPropertyIds: new List<VaultPropertyDefinitionId>(),
            RemovedPropertyIds: new List<VaultPropertyDefinitionId>(),
            Filenames: null,
            AddedPropertyNames: _fixture.CreateMany<string>(),
            RemovedPropertyNames: _fixture.CreateMany<string>());
        UpdateFilePropertyDefinitionsHandler sut = new(mediator.Object, vaultService.Object);

        XDocument vaultServiceResponse = sut.Serializer.Serialize(files);
        mediator.Setup(x => x.Send(It.IsAny<GetAllPropertyDefinitionInfosQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_fixture.CreateMany<VaultProperty>());

        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(vaultServiceResponse);

        // Act
        IEnumerable<VaultFile> result = await sut.Handle(command, CancellationToken.None);

        // Assert
        mediator.Verify(x => x.Send(It.IsAny<FindFilesBySearchConditionsQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        mediator.Verify(x => x.Send(It.IsAny<GetAllPropertyDefinitionInfosQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once);
    }
}
