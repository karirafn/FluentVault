using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultPropertyMappingType : SmartEnum<VaultPropertyMappingType>
{
    public static readonly VaultPropertyMappingType Constant = new(nameof(Constant), 1);
    public static readonly VaultPropertyMappingType Default = new(nameof(Default), 2);

    private VaultPropertyMappingType(string name, int value) : base(name, value) { }
}
