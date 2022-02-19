namespace FluentVault;

internal class SignOutRequest : SessionRequest
{
    public SignOutRequest(VaultSession session)
        : base(session, RequestData.SignOut) { }

    public async Task SendAsync()
    {
        string innerBody = GetOpeningTag(isSelfClosing: true);
        string requestBody = BodyBuilder.GetRequestBody(innerBody, Session.Ticket, Session.UserId);

        _ = await SendAsync(requestBody);
    }
}
