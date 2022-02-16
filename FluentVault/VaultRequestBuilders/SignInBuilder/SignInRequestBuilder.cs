using System.Text;
using System.Xml.Linq;

namespace FluentVault;

internal class SignInRequestBuilder : ISignInRequestBuilder, IWithCredentials
{
    private string _server = string.Empty;
    private string _database = string.Empty;

    public IWithCredentials ToVault(string server, string database)
    {
        ValidateVaultDetails(server, database);

        _server = server;
        _database = database;
        return this;
    }

    public async Task<Vault> WithCredentials(string username, string password)
    {
        ValidateCredentials(username, password);

        string body = GetSignInRequestBody(_server, _database, username, password);
        Uri uri = new($"http://{_server}/AutodeskDM/Services/Filestore/v26/AuthService.svc");
        string soapAction = @"""http://AutodeskDM/Filestore/Auth/1/7/2020/AuthService/SignIn""";

        XDocument document = await VaultHttpClient.SendRequestAsync(uri, body, soapAction);

        string t = document.GetElementValue("Ticket");
        string u = document.GetElementValue("UserId");

        ValidateOutput(t, u, out Guid ticket, out long userId);

        return new Vault(_server, _database, ticket, userId);
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

    private static void ValidateCredentials(string username, string password)
    {
        ArgumentNullException.ThrowIfNull(username, nameof(username));
        ArgumentNullException.ThrowIfNull(password, nameof(password));

        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Invalid username.", nameof(username));
        // password can be empty string
    }

    private static void ValidateOutput(string t, string u, out Guid ticket, out long userId)
    {
        if (Guid.TryParse(t, out ticket) == false)
            throw new ArgumentException("Failed to parse ticket.", nameof(ticket));

        if (long.TryParse(u, out userId) == false)
            throw new ArgumentException("Failed to parse user ID.", nameof(userId));
    }

    private static string GetSignInRequestBody(string server, string database, string username, string password)
    {
        var innerBody = GetSignInInnerBody(server, database, username, password);
        var requestBody = BodyBuilder.GetRequestBody(innerBody);

        return requestBody;
    }

    private static string GetSignInInnerBody(string server, string database, string username, string password)
    {
        StringBuilder bodyBuilder = new();
        bodyBuilder.AppendLine(@"<SignIn xmlns=""http://AutodeskDM/Filestore/Auth/1/7/2020/"">");
        bodyBuilder.AppendLine($"<dataServer>http://{server}</dataServer>");
        bodyBuilder.AppendLine($"<knowledgeVault>{database}</knowledgeVault>");
        bodyBuilder.AppendLine($"<userName>{username}</userName>");
        bodyBuilder.AppendLine($"<userPassword>{password}</userPassword>");
        bodyBuilder.AppendLine("</SignIn>");

        return bodyBuilder.ToString();
    }
}
