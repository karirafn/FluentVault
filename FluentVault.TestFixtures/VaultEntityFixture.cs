using System.Xml.Linq;

using AutoFixture;

namespace FluentVault.TestFixtures;
public abstract class VaultEntityFixture<T>
{
    public VaultEntityFixture(XNamespace @namespace)
    {
        Namespace = @namespace;
    }

    protected XNamespace Namespace { get; init; }
    protected Fixture Fixture { get; set; } = new();

    public T Create() => Fixture.Create<T>();
    public IEnumerable<T> CreateMany(int count) => Fixture.CreateMany<T>(count);
    protected XElement ParseXElement(string rootName, IEnumerable<T> entities)
    {
        XElement element = new(Namespace + rootName);
        foreach (T entity in entities)
            element.Add(ParseXElement(entity));

        return element;
    }

    public abstract XElement ParseXElement(T entity);
}
