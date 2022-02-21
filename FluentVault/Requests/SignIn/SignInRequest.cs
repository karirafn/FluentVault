using System.Text;
using System.Xml.Linq;

using FluentVault.Common.Extensions;
using FluentVault.Common.Helpers;

namespace FluentVault.Requests.SignIn;

internal class SignInRequest : BaseRequest, ISignInRequest, IWithCredentials
{
    private string _server = string.Empty;
    private string _database = string.Empty;

    public SignInRequest() : base(RequestData.SignIn) { }

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

        StringBuilder innerBody = GetInnerBodyBuilder(_server, _database, username, password);

        XDocument document = await SendRequestAsync(innerBody, _server);

        string t = document.GetElementValue("Ticket");
        string u = document.GetElementValue("UserId");

        ValidateResults(t, u, out Guid ticket, out long userId);

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

    private static void ValidateResults(string t, string u, out Guid ticket, out long userId)
    {
        if (Guid.TryParse(t, out ticket) == false)
            throw new ArgumentException("Failed to parse ticket.", nameof(ticket));

        if (long.TryParse(u, out userId) == false)
            throw new ArgumentException("Failed to parse user ID.", nameof(userId));
    }

    private StringBuilder GetInnerBodyBuilder(string server, string database, string username, string password)
        => new StringBuilder()
            .AppendElementWithAttribute(RequestData.Name, "xmlns", RequestData.Namespace)
            .AppendElement("dataServer", $"http://{server}")
            .AppendElement("knowledgeVault", database)
            .AppendElement("userName", username)
            .AppendElement("userPassword", password)
            .AppendElementClosing(RequestData.Name);
}
