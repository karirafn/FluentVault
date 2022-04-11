using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures;
public abstract class VaultEntityRequestFixture<T> : VaultEntityFixture<T>
{
    public VaultEntityRequestFixture(string operation, XNamespace @namespace) : base(@namespace)
    {
        Operation = operation;
    }

    protected string Operation { get; init; }

    public string CreateBody(T entity) => ParseXDocument(entity).ToString();
    public string CreateBody(IEnumerable<T> entities) => ParseXDocument(entities).ToString();

    public virtual XDocument ParseXDocument(IEnumerable<T> entities)
    {
        IEnumerable<XElement> content = entities.Select(entity => ParseXElement(entity));

        return new XDocument().AddResponseContent(Operation, Namespace, content, null);
    }

    public virtual XDocument ParseXDocument(T entity)
    {
        XElement content = ParseXElement(entity);

        return new XDocument().AddResponseContent(Operation, Namespace, content, null);
    }
}
