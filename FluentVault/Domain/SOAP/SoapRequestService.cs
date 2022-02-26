using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
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

        string json = System.IO.File.ReadAllText("requestdata.json");
        _data = JsonSerializer.Deserialize<SoapRequestDataCollection>(json)?.SoapRequestData.ToDictionary(x => x.Name)
            ?? throw new Exception("Failed to parse SOAP data collection");
    }

    public async Task<XDocument> SendAsync(string requestName, string requestBody)
    {
        StringContent requestContent = new(requestBody);
        requestContent.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");

        Uri requestUri = GetUri(requestName);
        string soapAction = GetSoapAction(requestName);
        HttpRequestMessage requestMessage = new(HttpMethod.Post, requestUri);
        requestMessage.Content = requestContent;
        requestMessage.Headers.Add("SOAPAction", soapAction);

        HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);
        string responseContent = await response.Content.ReadAsStringAsync();
        XDocument document = XDocument.Parse(responseContent);

        return document;
    }

    public async Task<XDocument> SendAsync(string requestName, VaultSessionCredentials session)
        => await SendAsync(requestName, GetRequestBody(requestName, session).ToString());

    private string GetSoapAction(string requestName)
        => new StringBuilder().SoapActionStringBuilder(_data[requestName]).ToString();

    public string GetNamespace(string requestName)
        => new StringBuilder().AppendNamespace(_data[requestName]).ToString();

    private Uri GetUri(string requestName)
        => new(new StringBuilder().AppendRequestUri(_data[requestName]).ToString());

    private string GetRequestBody(string requestName, VaultSessionCredentials session)
        => new StringBuilder().AppendRequestBodyOpening(session)
            .AppendElementWithAttribute(requestName,
                "xmlns",
                new StringBuilder().AppendNamespace(_data[requestName]).ToString(),
                isSelfClosing: true)
            .ToString();
}
