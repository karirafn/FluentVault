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
public class GetAllCategoryConfigurationsHandlerShould
{
    private static readonly SmartEnumFixture _fixture = new();
    private static readonly GetCategoryConfigurationsByBehaviorNamesSerializer _serializer = new();

    [Fact]
    public async Task ValVaultService()
    {
        // Arrange
        int count = 5;
        IEnumerable<VaultCategory> expectation = _fixture.CreateMany<VaultCategory>(count);
        XDocument response = _serializer.Serialize(expectation);
        Mock<IVaultService> vaultService = new();

        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        GetAllCategoryConfigurationsQuery query = new();
        GetAllCategoryConfigurationsHandler sut = new(vaultService.Object);

        // Act
        IEnumerable<VaultCategory> result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectation);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once());
    }
}
