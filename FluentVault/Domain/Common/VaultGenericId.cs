namespace FluentVault.Common;

public abstract class VaultGenericId<T> : IEquatable<VaultGenericId<T>> where T : struct
{
    public VaultGenericId(T value) => Value = value;

    public T Value { get; }

    public bool Equals(VaultGenericId<T>? other) => Value.Equals(other?.Value);
    public bool Equals(T other) => Value.Equals(other);
    public override bool Equals(object? obj)
        => obj is VaultGenericId<T> id && Equals(id) || obj is T type && Equals(type);
    public override string ToString() => Value.ToString() ?? string.Empty;
    public override int GetHashCode() => Value.GetHashCode();
}

