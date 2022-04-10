using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Features;
using FluentVault.TestFixtures.User;
using FluentVault.UnitTests.Helpers;

using Moq;

using Xunit;

namespace FluentVault.UnitTests.Systems.Features;
public class GatUserInfosByUserIdsHandlerShould
{
    private static readonly VaultUserInfoFixture _fixture = new();

    [Fact]
    public async Task ValVaultService()
    {
        // Arrange
        VaultUserInfo expectation = _fixture.Create();
        XDocument response = _fixture.ParseXDocument(expectation);
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
