using System.Xml.Linq;

namespace FluentVault.Common;

internal interface IVaultEntitySerializer<T>
{
    string ElementName { get; }
    XNamespace Namespace { get; }
    XElement Serialize(T entity);
    IEnumerable<XElement> Serialize(IEnumerable<T>? entities);
    XElement SerializeMany(string name, IEnumerable<T> entities);
    T Deserialize(XElement element);
    IEnumerable<T> DeserializeMany(XElement element);
}
