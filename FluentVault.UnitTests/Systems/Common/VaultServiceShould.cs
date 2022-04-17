using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using AutoFixture;

using FluentVault.Common;

using MediatR;

using Microsoft.Extensions.Options;

using Moq;
using Moq.Protected;

using Xunit;

namespace FluentVault.UnitTests.Systems.Common;

public class VaultServiceShould
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IHttpClientFactory> _httpClientFactory = new();
    private readonly IOptions<VaultOptions> _options;

    public VaultServiceShould()
    {
        _options = Options.Create(_fixture.Create<VaultOptions>());
    }

    [Fact]
    public async Task ThrowHttpResponseException_WhenResponseStatusCodeIsNotOk()
    {
        // Arrange
        VaultRequest request = _fixture.Create<VaultRequest>();
        HttpResponseMessage response = new(HttpStatusCode.NotFound);
        Mock<IMediator> mediator = new();
        Mock<HttpMessageHandler> handler = new();

        handler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        HttpClient httpClient = new(handler.Object) { BaseAddress = new Uri("http://server") };

        _httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);

        VaultService sut = new(_httpClientFactory.Object, _options, mediator.Object);

        // Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => sut.SendAsync(request, canSignIn: false));
    }
}
