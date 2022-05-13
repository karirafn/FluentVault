using System.Net;
using System.Net.Http.Headers;
using System.Xml.Linq;

using MediatR;

namespace FluentVault.Common;
internal class VaultService : IVaultService
{
    private readonly HttpClient _httpClient;

    public VaultService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Vault");
    }

    public async Task<XDocument> SendAsync(string uri, string soapAction, XDocument requestBody, CancellationToken cancellationToken = default)
    {
        HttpRequestMessage requestMessage = GetRequestMessage(uri, soapAction, requestBody);
        HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage, cancellationToken);

        if (responseMessage.StatusCode != HttpStatusCode.OK)
            throw new HttpRequestException("Invalid HTTP response", null, responseMessage.StatusCode);

        string responseContent = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        XDocument document = XDocument.Parse(responseContent);

        return document;
    }

    private static HttpRequestMessage GetRequestMessage(string uri, string soapAction, XDocument requestBody)
    {
        StringContent requestContent = new(requestBody.ToString());
        requestContent.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");

        HttpRequestMessage requestMessage = new(HttpMethod.Post, uri);
        requestMessage.Content = requestContent;
        requestMessage.Headers.Add("SOAPAction", $@"""{soapAction}""");

        return requestMessage;
    }
}
