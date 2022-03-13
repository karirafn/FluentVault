using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultMappingType : SmartEnum<VaultMappingType>
{
    public static readonly VaultMappingType Constant = new(nameof(Constant), 1);
    public static readonly VaultMappingType Default = new(nameof(Default), 2);

    private VaultMappingType(string name, int value) : base(name, value) { }
}
