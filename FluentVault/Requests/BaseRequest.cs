using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

using FluentVault.Common.Extensions;
using FluentVault.Common.Helpers;

namespace FluentVault.Requests;

internal abstract class BaseRequest
{
    protected readonly RequestData RequestData;

    public BaseRequest(RequestData requestData) => RequestData = requestData;

    public async Task<XDocument> SendRequestAsync(StringBuilder innerBody, string server, Guid ticket = new(), long userId = 0)
    {
        Uri uri = RequestData.GetUri(server);
        string requestBody = GetRequestBody(innerBody, ticket, userId);
        StringContent content = GetRequestContent(requestBody);
        HttpRequestMessage request = GetRequestMessage(uri, content);

        using HttpClient httpClient = new();
        HttpResponseMessage response = await httpClient.SendAsync(request);
        string responseBody = await response.Content.ReadAsStringAsync();

        XDocument document = XDocument.Parse(responseBody);
        return document;
    }

    public StringBuilder GenerateInnerBodyFromRequestData()
        => new StringBuilder()
            .AppendElementWithAttribute(RequestData.Name, "xmlns", RequestData.Namespace, isSelfClosing: true);

    private static string GetRequestBody(StringBuilder innerBody, Guid ticket, long userId)
        => new StringBuilder()
            .AppendRequestBody(innerBody, ticket, userId)
            .ToString();

    private HttpRequestMessage GetRequestMessage(Uri uri, StringContent content)
    {
        HttpRequestMessage request = new(HttpMethod.Post, uri);
        request.Content = content;
        request.Headers.Add("SOAPAction", RequestData.SoapAction);
        return request;
    }

    private static StringContent GetRequestContent(string requestBody)
    {
        StringContent content = new(requestBody);
        content.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");
        return content;
    }
}
