namespace FluentVault;

public class Vault : IAsyncDisposable
{
    private readonly VaultSession _session;

    internal Vault(string server, string database, Guid ticket, long userId)
    {
        _session = new(server, database, ticket, userId);
        Server = server;
        Database = database;
        Ticket = ticket;
        UserId = userId;
    }

    public string Server { get; }
    public string Database { get; }
    public Guid Ticket { get; }
    public long UserId { get; }

    public static ISignInRequest SignIn => new SignInRequest();
    public async Task SignOut() => await new SignOutRequest(_session).SendAsync();

    public IGetRequest Get => new GetRequest(_session);
    public ISearchRequest Search => new SearchRequest(_session);
    public IUpdateRequest Update => new UpdateRequest(_session);

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await SignOut();
        GC.SuppressFinalize(this);
    }
}
