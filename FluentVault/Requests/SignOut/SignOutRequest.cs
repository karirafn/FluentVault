using System.Text;

using FluentVault.Common.Helpers;

namespace FluentVault.Requests.SignOut;

internal class SignOutRequest : SessionRequest
{
    public SignOutRequest(VaultSession session)
        : base(session, RequestData.SignOut) { }

    public async Task SendAsync()
    {
        StringBuilder innerBody = GenerateInnerBodyFromRequestData();
        _ = await SendRequestAsync(innerBody);
    }
}
