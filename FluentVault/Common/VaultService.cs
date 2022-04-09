using System.Net;
using System.Net.Http.Headers;
using System.Xml.Linq;

using FluentVault.Extensions;
using FluentVault.Features;

using Microsoft.Extensions.Options;

namespace FluentVault.Common;

internal class VaultService : IVaultService, IAsyncDisposable
{
    private readonly HttpClient _httpClient;
    private readonly VaultOptions _options;
    private readonly IVaultRequestData _data;
    private VaultSessionCredentials _session = new();

    public VaultService(IHttpClientFactory httpClientFactory, IOptions<VaultOptions> options, IVaultRequestData data)
    {
        _httpClient = httpClientFactory.CreateClient("Vault");
        _options = options.Value;
        _data = data;
    }

    public async Task<XDocument> SendAsync(string operation, bool canSignIn, Action<XElement, XNamespace>? contentBuilder = null, CancellationToken cancellationToken = default)
    {
        if (canSignIn && _session.IsValid == false)
            _session = await new SignInHandler(this).Handle(new SignInCommand(_options), cancellationToken);

        HttpRequestMessage requestMessage = GetRequestMessage(operation, _session, contentBuilder);
        HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage, cancellationToken);

        if (responseMessage.StatusCode != HttpStatusCode.OK)
            throw new HttpRequestException("Invalid HTTP response", null, responseMessage.StatusCode);

        string responseContent = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        XDocument document = XDocument.Parse(responseContent);

        return document;
    }

    private HttpRequestMessage GetRequestMessage(string operation, VaultSessionCredentials session, Action<XElement, XNamespace>? contentBuilder)
    {
        VaultRequest data = _data.Get(operation);
        string uri = data.Uri;
        string soapAction = data.SoapAction;
        XDocument requestBody = GetRequestBody(data.Operation, data.Namespace, session, contentBuilder);
        StringContent requestContent = GetRequestContent(requestBody);

        HttpRequestMessage requestMessage = new(HttpMethod.Post, uri);
        requestMessage.Content = requestContent;
        requestMessage.Headers.Add("SOAPAction", $@"""{soapAction}""");

        return requestMessage;
    }

    private static StringContent GetRequestContent(XDocument requestBody)
    {
        StringContent requestContent = new(requestBody.ToString());
        requestContent.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");

        return requestContent;
    }

    private static XDocument GetRequestBody(string operation, string @namespace, VaultSessionCredentials session, Action<XElement, XNamespace>? contentBuilder)
    {
        XNamespace ns = $"{@namespace}/";
        XElement content = new(ns + operation);

        if (contentBuilder is not null)
            contentBuilder.Invoke(content, ns);

        XDocument requestBody = new XDocument().AddRequestBody(session, content);

        return requestBody;
    }

    public async ValueTask DisposeAsync()
    {
        if (_session.IsValid)
            await new SignOutHandler(this).Handle(new SignOutCommand(), default);

        GC.SuppressFinalize(this);
    }
}
