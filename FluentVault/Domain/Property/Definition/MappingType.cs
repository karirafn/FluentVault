using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class MappingType : SmartEnum<MappingType>
{
    public static readonly MappingType Constant = new(nameof(Constant), 1);
    public static readonly MappingType Default = new(nameof(Default), 2);

    private MappingType(string name, int value) : base(name, value) { }
}
