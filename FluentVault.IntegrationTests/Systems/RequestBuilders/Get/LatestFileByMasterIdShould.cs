using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;
using FluentVault.RequestBuilders;

using MediatR;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.RequestBuilders.Get;
public class LatestFileByMasterIdShould
{
    private static readonly VaultTestData _testData = new();

    [Fact]
    public async Task ReturnFile()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IMediator mediator = provider.GetRequiredService<IMediator>();
        GetRequestBuilder sut = new(mediator);

        // Act
        VaultFile result = await sut.LatestFileByMasterId(new(_testData.TestPartMasterId), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
    }
}
