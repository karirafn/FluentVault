using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using AutoFixture;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Features;
using FluentVault.TestFixtures;
using FluentVault.UnitTests.Helpers;

using MediatR;

using Moq;

using Xunit;

namespace FluentVault.UnitTests.Systems.Features;
public class GetUserInfosByUserIdsHandlerShould
{
    private static readonly SmartEnumFixture _fixture = new();

    [Fact]
    public async Task CallVaultService()
    {
        // Arrange
        VaultUserInfo expectation = _fixture.Create<VaultUserInfo>();
        Mock<IMediator> mediator = new();
        Mock<IVaultService> vaultService = new();

        GetUserInfosByIserIdsQuery query = new(new[] { expectation.User.Id });
        GetUserInfosByUserIdsHandler sut = new(mediator.Object, vaultService.Object);

        XDocument response = sut.Serializer.Serialize(expectation);
        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        // Act
        IEnumerable<VaultUserInfo> result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Single().Should().BeEquivalentTo(expectation);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once());
    }
}
