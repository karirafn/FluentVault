using System.Net.Http.Headers;

namespace FluentVault;

internal class VaultStringContent : StringContent
{
    internal VaultStringContent(string content) : base(content)
    {
        Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");
    }
}
