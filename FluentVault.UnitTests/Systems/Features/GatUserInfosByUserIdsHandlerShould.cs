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

using Moq;

using Xunit;

namespace FluentVault.UnitTests.Systems.Features;
public class GatUserInfosByUserIdsHandlerShould
{
    private static readonly SmartEnumFixture _fixture = new();
    private static readonly GetUserInfosByUserIdsSerializer _serializer = new();

    [Fact]
    public async Task CallVaultService()
    {
        // Arrange
        VaultUserInfo expectation = _fixture.Create<VaultUserInfo>();
        XDocument response = _serializer.Serialize(expectation);
        Mock<IVaultService> vaultService = new();

        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        GetUserInfosByIserIdsQuery query = new(new[] { expectation.User.Id });
        GetUserInfosByUserIdsHandler sut = new(vaultService.Object);

        // Act
        IEnumerable<VaultUserInfo> result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Single().Should().BeEquivalentTo(expectation);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once());
    }
}
