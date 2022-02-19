using System.Net.Http.Headers;
using System.Xml.Linq;

namespace FluentVault;

internal abstract class BaseRequest
{
    protected readonly RequestData RequestData;

    public BaseRequest(RequestData requestData) => RequestData = requestData;

    protected string GetOpeningTag(bool isSelfClosing = false)
        => $@"<{RequestData.Name} xmlns=""{RequestData.Namespace}""{(isSelfClosing ? "/>" : ">")}";

    protected string GetClosingTag() => $"</{RequestData.Name}>";

    public async Task<XDocument> SendAsync(Uri uri, string requestBody)
    {
        StringContent content = new(requestBody);
        content.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");

        HttpRequestMessage request = new(HttpMethod.Post, uri);
        request.Content = content;
        request.Headers.Add("SOAPAction", RequestData.SoapAction);

        using HttpClient httpClient = new();
        HttpResponseMessage response = await httpClient.SendAsync(request);
        string responseBody = await response.Content.ReadAsStringAsync();

        var document = XDocument.Parse(responseBody);

        return document;
    }
}
