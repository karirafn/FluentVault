﻿namespace FluentVault;

public class EntityClass : BaseType
{
    public static readonly EntityClass File = new("FILE");
    public static readonly EntityClass Folder = new("FLDR");
    public static readonly EntityClass Item = new("ITEM");

    private EntityClass(string value) : base(value) { }

    public static EntityClass Parse(string value)
        => Parse(value, new[] { File, Folder, Item });
}
