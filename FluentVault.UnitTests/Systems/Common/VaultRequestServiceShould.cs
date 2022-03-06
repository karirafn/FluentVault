using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using AutoFixture;

using FluentVault.Common;
using FluentVault.Domain;

using Moq;
using Moq.Protected;

using Xunit;

namespace FluentVault.UnitTests.Systems.Common;

public class VaultRequestServiceShould
{
    private static readonly Fixture _fixture = new();
    private readonly Mock<IHttpClientFactory> _httpClientFactory = new();

    [Fact]
    public async Task ThrowKeyNotFoundException_WhenGivenAnInvalidRequestName()
    {
        // Arrange
        string requestName = "This request name is definitely invalid.";
        VaultRequestService sut = new(_httpClientFactory.Object);

        // Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => sut.SendAsync(requestName, It.IsAny<VaultSessionCredentials>()));
    }

    [Fact]
    public async Task ThrowHttpResponseException_WhenResponseStatusCodeIsNotOk()
    {
        // Arrange
        string operation = VaultRequestDataCollection.SoapRequestData.First().Operation;
        VaultSessionCredentials session = _fixture.Create<VaultSessionCredentials>();
        HttpResponseMessage response = new(HttpStatusCode.NotFound);
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
        VaultRequestService sut = new(_httpClientFactory.Object);

        // Act

        // Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => sut.SendAsync(operation, session));
    }
}
