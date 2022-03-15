using System.Net;
using System.Net.Http.Headers;
using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.Common;

internal class VaultRequestService : IVaultRequestService
{
    private readonly HttpClient _httpClient;
    private readonly IDictionary<string, VaultRequestData> _data;

    public VaultRequestService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Vault");
        _data = VaultRequestDataCollection.SoapRequestData.ToDictionary(x => x.Operation);
    }

    public async Task<XDocument> SendAsync(string operation, VaultSessionCredentials session, Action<XElement, XNamespace>? contentBuilder = null, CancellationToken cancellationToken = default)
    {
        if (_data.Keys.Any(key => key == operation) is false)
            throw new KeyNotFoundException($@"Operation ""{operation}"" was not found in Vault request data collection");

        HttpRequestMessage requestMessage = GetRequestMessage(operation, session, contentBuilder);
        HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage, cancellationToken);

        if (responseMessage.StatusCode != HttpStatusCode.OK)
            throw new HttpRequestException("Invalid HTTP response", null, responseMessage.StatusCode);

        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        XDocument document = XDocument.Parse(responseContent);

        return document;
    }

    private HttpRequestMessage GetRequestMessage(string operation, VaultSessionCredentials session, Action<XElement, XNamespace>? contentBuilder)
    {
        string uri = _data[operation].Uri;
        string soapAction = _data[operation].SoapAction;
        XDocument requestBody = GetRequestBody(operation, session, contentBuilder);
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

    private XDocument GetRequestBody(string operation, VaultSessionCredentials session, Action<XElement, XNamespace>? contentBuilder)
    {
        XNamespace ns = $"{_data[operation].Namespace}/";
        XElement content = new(ns + operation);

        if (contentBuilder is not null)
            contentBuilder.Invoke(content, ns);

        XDocument requestBody = new XDocument().AddRequestBody(session, content);

        return requestBody;
    }
}
