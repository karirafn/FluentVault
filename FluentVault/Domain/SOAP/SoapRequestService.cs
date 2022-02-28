using System.Net;
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

        string json = ReadRequestDataFromFile();
        _data = JsonSerializer.Deserialize<SoapRequestDataCollection>(json)?
            .SoapRequestData
            .ToDictionary(x => x.Name)
            ?? throw new Exception("Failed to parse SOAP data collection");
    }

    public async Task<XDocument> SendAsync(string requestName, VaultSessionCredentials session, Action<XElement, XNamespace>? contentBuilder = null)
    {
        XDocument requestBody = GetRequestBody(requestName, session, contentBuilder);
        StringContent requestContent = GetRequestContent(requestBody);
        HttpRequestMessage requestMessage = GetRequestMessage(requestName, requestContent);

        HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage);

        if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            throw new Exception("Invalid request, status code is 404 Not Found");

        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        XDocument document = XDocument.Parse(responseContent);

        return document;
    }

    private static string ReadRequestDataFromFile()
    {
        string directory = Path.GetDirectoryName(typeof(VaultClient).Assembly.Location)
            ?? throw new Exception("Failed to get assembly location");
        string path = Path.Combine(directory, "requestdata.json");
        string json = File.ReadAllText(path);

        return json;
    }

    private HttpRequestMessage GetRequestMessage(string requestName, StringContent requestContent)
    {
        string uri = UriBuilder(requestName).ToString();
        string soapAction = SoapActionBuilder(requestName).ToString();
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
        XNamespace ns = NamespaceBuilder(requestName).ToString();
        XElement content = new(ns + requestName);

        if (contentBuilder is not null)
            contentBuilder.Invoke(content, ns);

        XDocument requestBody = new XDocument().AddRequestBody(session, content);

        return requestBody;
    }

    private StringBuilder SoapActionBuilder(string requestName)
        => new StringBuilder()
            .Append("http://AutodeskDM/")
            .Append(NamespaceBuilder(requestName));

    private StringBuilder NamespaceBuilder(string requestName)
        => new StringBuilder().Append("http://AutodeskDM/").Append(_data[requestName].Namespace);

    private StringBuilder UriBuilder(string requestName)
        => new StringBuilder().Append("AutodeskDM/Services/")
            .Append(_data[requestName].Version)
            .Append('/')
            .Append(_data[requestName].Service)
            .Append(".svc")
            .AppendRequestCommand(_data[requestName].Name, _data[requestName].Command);
}
