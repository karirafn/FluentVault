namespace FluentVault;

public class AllowedMappingDirection : BaseType
{
    public static readonly AllowedMappingDirection Read = new(nameof(Read));
    public static readonly AllowedMappingDirection Write = new(nameof(Write));
    public static readonly AllowedMappingDirection ReadAndWrite = new(nameof(ReadAndWrite));
    public static readonly AllowedMappingDirection None = new(nameof(None));

    private AllowedMappingDirection(string value) : base(value) { }

    public static AllowedMappingDirection Parse(string value)
        => Parse(value, new[] { Read, Write, ReadAndWrite, None });
}
