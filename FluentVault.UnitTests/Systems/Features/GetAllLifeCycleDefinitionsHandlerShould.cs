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
public class GetAllLifeCycleDefinitionsHandlerShould
{
    private static readonly SmartEnumFixture _fixture = new();
    private static readonly GetAllLifeCycleDefinitionsSerializer _serializer = new();

    [Fact]
    public async Task ValVaultService()
    {
        // Arrange
        int count = 5;
        IEnumerable<VaultLifeCycleDefinition> expectation = _fixture.CreateMany<VaultLifeCycleDefinition>(count);
        XDocument response = _serializer.Serialize(expectation);
        Mock<IVaultService> vaultService = new();

        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        GetAllLifeCycleDefinitionsQuery query = new();
        GetAllLifeCycleDefinitionsHandler sut = new(vaultService.Object);

        // Act
        IEnumerable<VaultLifeCycleDefinition> result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectation);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once());
    }
}
