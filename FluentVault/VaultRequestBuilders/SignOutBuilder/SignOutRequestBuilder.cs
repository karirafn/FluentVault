namespace FluentVault;

public class SignOutRequestBuilder :
    ISignOutRequestBuilder,
    IWithSessionCredentials
{
    private string? _server;
    private string? _database;

    public IWithSessionCredentials FromVault(string server, string database)
    {
        ValidateVaultDetails(server, database);

        _server = server;
        _database = database;
        return this;
    }

    public async Task WithSessionCredentials(Guid ticket, long userId)
    {
        ValidateSessionCredentials(ticket, userId);

        var body = GetSignOutRequestBody(ticket, userId);
        var uri = new Uri($"http://{_server}/AutodeskDM/Services/Filestore/v26_2/AuthService.svc?op=SignOut&uid=8&currentCommand=Connectivity.Application.VaultBase.SignOutCommand&vaultName={_database}&app=VP");
        var soapAction = @"""http://AutodeskDM/Filestore/Auth/1/8/2021/AuthService/SignOut""";

        _ = await VaultHttpClient.SendRequestAsync(uri, body, soapAction);
    }

    private static void ValidateVaultDetails(string server, string database)
    {
        ArgumentNullException.ThrowIfNull(server, nameof(server));
        ArgumentNullException.ThrowIfNull(database, nameof(database));

        if (string.IsNullOrWhiteSpace(server))
            throw new ArgumentException("Invalid server name.", nameof(server));

        if (string.IsNullOrWhiteSpace(database))
            throw new ArgumentException("Invalid database name.", nameof(database));
    }

    private static void ValidateSessionCredentials(Guid ticket, long userId)
    {
        ArgumentNullException.ThrowIfNull(ticket, nameof(ticket));
        ArgumentNullException.ThrowIfNull(userId, nameof(userId));

        if (userId < 1)
            throw new ArgumentOutOfRangeException(nameof(userId), "User id must be greater than zero.");
    }

    private static string GetSignOutRequestBody(Guid ticket, long userId)
    {
        var innerBody = @"      <SignOut xmlns=""http://AutodeskDM/Filestore/Auth/1/8/2021/""/>";
        var requestBody = BodyBuilder.GetRequestBody(innerBody, ticket, userId);

        return requestBody;
    }
}
