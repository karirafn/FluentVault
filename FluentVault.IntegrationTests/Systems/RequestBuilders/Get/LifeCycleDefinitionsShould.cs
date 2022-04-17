using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;
using FluentVault.RequestBuilders;

using MediatR;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.RequestBuilders.Get;
public class LifeCycleDefinitionsShould
{
    [Fact]
    public async Task ReturnAllLifeCycleDefinitions()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IMediator mediator = provider.GetRequiredService<IMediator>();
        GetRequestBuilder sut = new(mediator);

        // Act
        IEnumerable<VaultLifeCycleDefinition> result = await sut.LifeCycleDefinitions(CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
    }
}
