using Ardalis.SmartEnum;

namespace FluentVault;
public abstract class VaultClientType : SmartEnum<VaultClientType>
{
    public static readonly VaultClientType Thick = new ThickClientType();
    public static readonly VaultClientType Thin = new ThinClientType();

    private VaultClientType(string name, int value) : base(name, value) { }

    public abstract Uri GetUri(string server, string database, string objectId, string objectType);

    private class ThickClientType : VaultClientType
    {
        public ThickClientType() : base(nameof(Thick), 1) { }

        public override Uri GetUri(string server, string database, string objectId, string objectType) =>
            new($"http://{server}/AutodeskDM/Services/EntityDataCommandRequest.aspx?Vault={database}&ObjectId={objectId}&ObjectType={objectType}&Command=Select");
    }

    private class ThinClientType : VaultClientType
    {
        public ThinClientType() : base(nameof(Thin), 2) { }

        public override Uri GetUri(string server, string database, string objectId, string objectType) =>
            new($"http://{server}/AutodeskTC/{database}/Explore/{objectType}/{objectId}");
    }
}
