using System.Net.Http.Headers;
using System.Xml.Linq;

namespace FluentVault;

internal abstract class BaseRequest
{
    public BaseRequest(string name)
    {
        RequestName = name;
    }

    public string RequestName { get; init; }

    public string GetOpeningTag(bool isSelfClosing = false)
        => isSelfClosing
        ? $@"<{RequestName} xmlns=""{RequestData.GetNamespace(RequestName)}""/>"
        : $@"<{RequestName} xmlns=""{RequestData.GetNamespace(RequestName)}"">";

    public string GetClosingTag() => $"</{RequestName}>";

    public async Task<XDocument> SendAsync(Uri uri, string requestBody)
    {
        string soapAction = RequestData.GetSoapAction(RequestName);

        StringContent content = new(requestBody);
        content.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");

        HttpRequestMessage request = new(HttpMethod.Post, uri);
        request.Content = content;
        request.Headers.Add("SOAPAction", soapAction);

        using HttpClient httpClient = new();
        HttpResponseMessage response = await httpClient.SendAsync(request);
        string responseBody = await response.Content.ReadAsStringAsync();

        var document = XDocument.Parse(responseBody);

        return document;
    }
}
