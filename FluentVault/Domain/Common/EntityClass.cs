using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class EntityClass : SmartEnum<EntityClass>
{
    public static readonly EntityClass File = new(nameof(File).ToUpper(), 1);
    public static readonly EntityClass Folder = new("FLDR", 2);
    public static readonly EntityClass Item = new(nameof(Item).ToUpper(), 3);
    public static readonly EntityClass CustomEntity = new("CUSTENT", 4);

    private EntityClass(string name, int value) : base(name, value) { }
}
