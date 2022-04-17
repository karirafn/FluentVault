using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultCategorySerializer : XElementSerializer<VaultCategory>
{
    private const string Cat = nameof(Cat);
    private const string SysName = nameof(SysName);
    private const string Descr = nameof(Descr);
    private const string EntClassIdArray = nameof(EntClassIdArray);
    private const string EntClassId = nameof(EntClassId);

    public VaultCategorySerializer(XNamespace @namespace) : base(Cat, @namespace) { }

    internal override VaultCategory Deserialize(XElement element)
        => new(element.ParseElementValue(nameof(VaultCategory.Id), VaultCategoryId.Parse),
            element.GetElementValue(nameof(VaultCategory.Name)),
            element.GetElementValue(SysName),
            element.ParseElementValue(nameof(VaultCategory.Color), long.Parse),
            element.GetElementValue(Descr),
            element.ParseAllElementValues(EntClassId, x => VaultEntityClass.FromName(x)));

    internal override XElement Serialize(VaultCategory entity)
        => BaseElement
            .AddElement(Namespace, nameof(VaultCategory.Id), entity.Id)
            .AddElement(Namespace, nameof(VaultCategory.Name), entity.Name)
            .AddElement(Namespace, SysName, entity.SystemName)
            .AddElement(Namespace, nameof(VaultCategory.Color), entity.Color)
            .AddElement(Namespace, Descr, entity.Description)
            .AddNestedElements(Namespace, EntClassIdArray, EntClassId, entity.EntityClasses);
}
