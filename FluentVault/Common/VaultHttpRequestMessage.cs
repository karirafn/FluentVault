namespace FluentVault;

internal class VaultHttpRequestMessage : HttpRequestMessage
{
    internal VaultHttpRequestMessage(Uri uri, VaultStringContent content, string soapAction) : base(HttpMethod.Post, uri)
    {
        Content = content;
        Headers.Add("SOAPAction", soapAction);
    }
}
