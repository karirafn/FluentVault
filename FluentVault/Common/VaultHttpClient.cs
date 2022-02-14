using System.Xml.Linq;

namespace FluentVault;

internal static class VaultHttpClient
{
    public static async Task<XDocument> SendRequestAsync(Uri uri, string requestBody, string soapAction)
    {
        var requestMessage = new VaultStringContent(requestBody)
            .CreateVaultHttpRequestMessage(uri, soapAction);

        using HttpClient httpClient = new();
        var response = await httpClient.SendAsync(requestMessage);
        var responseString = await response.Content.ReadAsStringAsync();

        var document = XDocument.Parse(responseString);

        return document;
    }
}
