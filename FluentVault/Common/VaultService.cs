using System.Net;
using System.Net.Http.Headers;
using System.Xml.Linq;

using FluentVault.Domain.SecurityHeader;
using FluentVault.Extensions;
using FluentVault.Features;

using MediatR;

namespace FluentVault.Common;
internal class VaultService : IVaultService, IAsyncDisposable
{
    private readonly HttpClient _httpClient;
    private readonly IMediator _mediator;
    private VaultSecurityHeader? _securityHeader;

    public VaultService(IHttpClientFactory httpClientFactory, IMediator mediator)
    {
        _httpClient = httpClientFactory.CreateClient("Vault");
        _mediator = mediator;
    }

    public async Task<XDocument> SendAuthenticatedAsync(VaultRequest request, Action<XElement, XNamespace>? contentBuilder = null, CancellationToken cancellationToken = default)
    {
        if (_securityHeader is null)
            _securityHeader = await _mediator.Send(new SignInCommand(), cancellationToken);

        XDocument document = await SendAsync(request, contentBuilder, cancellationToken);

        return document;
    }

    public async Task<XDocument> SendUnauthenticatedAsync(VaultRequest request, Action<XElement, XNamespace>? contentBuilder, CancellationToken cancellationToken = default)
        => await SendAsync(request, contentBuilder, cancellationToken);

    private async Task<XDocument> SendAsync(VaultRequest request, Action<XElement, XNamespace>? contentBuilder = null, CancellationToken cancellationToken = default)
    {
        HttpRequestMessage requestMessage = GetRequestMessage(request, _securityHeader, contentBuilder);
        HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage, cancellationToken);

        if (responseMessage.StatusCode != HttpStatusCode.OK)
            throw new HttpRequestException("Invalid HTTP response", null, responseMessage.StatusCode);

        string responseContent = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        XDocument document = XDocument.Parse(responseContent);

        return document;
    }

    private static HttpRequestMessage GetRequestMessage(VaultRequest request, VaultSecurityHeader? securityHeader, Action<XElement, XNamespace>? contentBuilder)
    {
        XDocument requestBody = GetRequestBody(request, securityHeader, contentBuilder);
        StringContent requestContent = GetRequestContent(requestBody);

        HttpRequestMessage requestMessage = new(HttpMethod.Post, request.Uri);
        requestMessage.Content = requestContent;
        requestMessage.Headers.Add("SOAPAction", $@"""{request.SoapAction}""");

        return requestMessage;
    }

    private static StringContent GetRequestContent(XDocument requestBody)
    {
        StringContent requestContent = new(requestBody.ToString());
        requestContent.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");

        return requestContent;
    }

    private static XDocument GetRequestBody(VaultRequest request, VaultSecurityHeader? securityHeader, Action<XElement, XNamespace>? contentBuilder)
    {
        XNamespace ns = $"{request.Namespace}/";
        XElement content = new(ns + request.Operation);

        if (contentBuilder is not null)
            contentBuilder.Invoke(content, ns);

        XDocument requestBody = new XDocument().AddRequestBody(content, securityHeader);

        return requestBody;
    }

    public async ValueTask DisposeAsync()
    {
        await _mediator.Send(new SignOutCommand());
        GC.SuppressFinalize(this);
    }
}
