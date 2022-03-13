namespace FluentVault.Common;

public abstract class GenericId<T> : IEquatable<GenericId<T>> where T : struct
{
    public GenericId(T value) => Value = value;

    public T Value { get; }

    public bool Equals(GenericId<T>? other) => Value.Equals(other?.Value);
    public bool Equals(T other) => Value.Equals(other);
    public override bool Equals(object? obj)
        => obj is GenericId<T> id && Equals(id) || obj is T type && Equals(type);
    public override string ToString() => Value.ToString() ?? string.Empty;
    public override int GetHashCode() => Value.GetHashCode();
}

