using System.Net;
using System.Net.Http.Headers;
using System.Xml.Linq;

using FluentVault.Extensions;
using FluentVault.Features;

using MediatR;

using Microsoft.Extensions.Options;

namespace FluentVault.Common;

internal class VaultService : IVaultService, IAsyncDisposable
{
    private readonly HttpClient _httpClient;
    private readonly VaultOptions _options;
    private readonly IMediator _mediator;
    private VaultSessionCredentials _session = new();

    public VaultService(IHttpClientFactory httpClientFactory, IOptions<VaultOptions> options, IMediator mediator)
    {
        _httpClient = httpClientFactory.CreateClient("Vault");
        _options = options.Value;
        _mediator = mediator;
    }

    public async Task<XDocument> SendAsync(VaultRequest request, bool canSignIn, Action<XElement, XNamespace>? contentBuilder = null, CancellationToken cancellationToken = default)
    {
        if (canSignIn && _session.IsValid == false)
            _session = await _mediator.Send(new SignInCommand(_options), cancellationToken);

        HttpRequestMessage requestMessage = GetRequestMessage(request, _session, contentBuilder);
        HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage, cancellationToken);

        if (responseMessage.StatusCode != HttpStatusCode.OK)
            throw new HttpRequestException("Invalid HTTP response", null, responseMessage.StatusCode);

        string responseContent = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        XDocument document = XDocument.Parse(responseContent);

        return document;
    }

    private static HttpRequestMessage GetRequestMessage(VaultRequest request, VaultSessionCredentials session, Action<XElement, XNamespace>? contentBuilder)
    {
        XDocument requestBody = GetRequestBody(request, session, contentBuilder);
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

    private static XDocument GetRequestBody(VaultRequest request, VaultSessionCredentials session, Action<XElement, XNamespace>? contentBuilder)
    {
        XNamespace ns = $"{request.Namespace}/";
        XElement content = new(ns + request.Operation);

        if (contentBuilder is not null)
            contentBuilder.Invoke(content, ns);

        XDocument requestBody = new XDocument().AddRequestBody(session, content);

        return requestBody;
    }

    public async ValueTask DisposeAsync()
    {
        if (_session.IsValid)
            _ = await _mediator.Send(new SignOutCommand());

        GC.SuppressFinalize(this);
    }
}
