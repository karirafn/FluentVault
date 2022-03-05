using System.Net;
using System.Net.Http.Headers;
using System.Xml.Linq;

using FluentVault.Common.Extensions;

namespace FluentVault.Domain.SOAP;

internal class SoapRequestService : ISoapRequestService
{
    private readonly HttpClient _httpClient;
    private readonly IDictionary<string, SoapRequestData> _data;

    public SoapRequestService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Vault");

        _data = SoapRequestDataCollection.SoapRequestData.ToDictionary(x => x.Name);
    }

    public async Task<XDocument> SendAsync(string requestName, VaultSessionCredentials session, Action<XElement, XNamespace>? contentBuilder = null)
    {
        HttpRequestMessage requestMessage = GetRequestMessage(requestName, session, contentBuilder);

        HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage);

        if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            throw new Exception("Invalid request, status code is 404 Not Found");

        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        XDocument document = XDocument.Parse(responseContent);

        return document;
    }

    private HttpRequestMessage GetRequestMessage(string requestName, VaultSessionCredentials session, Action<XElement, XNamespace>? contentBuilder)
    {
        XDocument requestBody = GetRequestBody(requestName, session, contentBuilder);
        StringContent requestContent = GetRequestContent(requestBody);
        HttpRequestMessage requestMessage = GetRequestMessage(requestName, requestContent);
        return requestMessage;
    }

    private HttpRequestMessage GetRequestMessage(string requestName, StringContent requestContent)
    {
        string uri = _data[requestName].Uri;
        string soapAction = _data[requestName].SoapAction;
        HttpRequestMessage requestMessage = new(HttpMethod.Post, uri);
        requestMessage.Content = requestContent;
        requestMessage.Headers.Add("SOAPAction", soapAction);

        return requestMessage;
    }

    private static StringContent GetRequestContent(XDocument requestBody)
    {
        StringContent requestContent = new(requestBody.ToString());
        requestContent.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");

        return requestContent;
    }

    private XDocument GetRequestBody(string requestName, VaultSessionCredentials session, Action<XElement, XNamespace>? contentBuilder)
    {
        XNamespace ns = _data[requestName].Namespace;
        XElement content = new(ns + requestName);

        if (contentBuilder is not null)
            contentBuilder.Invoke(content, ns);

        XDocument requestBody = new XDocument().AddRequestBody(session, content);

        return requestBody;
    }
}
