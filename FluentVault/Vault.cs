namespace FluentVault;

public class Vault : IDisposable
{
    private readonly VaultSessionInfo _session;

    public string Server { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
    public Guid Ticket { get; set; }
    public long UserId { get; set; }

    public Vault(string server, string database, Guid ticket, long userId)
    {
        _session = new(server, database, ticket, userId);
        Server = server;
        Database = database;
        Ticket = ticket;
        UserId = userId;
    }

    public static ISignInRequestBuilder SignIn => new SignInRequestBuilder();
    public static ISignOutRequestBuilder SignOut => new SignOutRequestBuilder();

    public IGetRequestBuilder Get => new GetRequestBuilder(_session);
    public ISearchRequestBuilder Search => new SearchRequestBuilder(_session);
    public IUpdateRequestBuilder Update => new UpdateRequestBuilder(_session);

    public void Dispose()
    {
        SignOut.FromVault(_session.Server, _session.Database).WithSessionCredentials(Ticket, UserId);
        GC.SuppressFinalize(this);
    }
}
