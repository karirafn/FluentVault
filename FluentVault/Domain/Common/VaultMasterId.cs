
using System.Text.Json;
using System.Text.Json.Serialization;

using FluentVault.Common;

namespace FluentVault;

[JsonConverter(typeof(MasterIdConverter))]
public class VaultMasterId : VaultGenericId<long>
{
    public VaultMasterId(long value) : base(value) { }

    public static VaultMasterId Invalid = new(-1);

    public static VaultMasterId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse master ID."));

    class MasterIdConverter : JsonConverter<VaultMasterId>
    {
        public override VaultMasterId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.TryGetInt64(out long value) ? new VaultMasterId(value) : Invalid;

        public override void Write(Utf8JsonWriter writer, VaultMasterId masterId, JsonSerializerOptions options)
            => writer.WriteNumberValue(masterId.Value);
    }
}
