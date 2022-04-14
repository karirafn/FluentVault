using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultRoleSerializer : XElementSerializer<VaultRole>
{
    private const string Roles = nameof(Roles);
    private const string SysName = nameof(SysName);
    private const string IsSys = nameof(IsSys);
    private const string Descr = nameof(Descr);

    public VaultRoleSerializer(XNamespace @namespace) : base(Roles, @namespace) { }

    internal override VaultRole Deserialize(XElement element)
        => new(element.ParseAttributeValue(nameof(VaultRole.Id), x => VaultRoleId.Parse(x)),
            element.GetAttributeValue(nameof(VaultRole.Name)),
            element.GetAttributeValue(SysName),
            element.ParseAttributeValue(IsSys, bool.Parse),
            element.GetAttributeValue(Descr));

    internal override XElement Serialize(VaultRole role)
        => BaseElement
            .AddAttribute(nameof(VaultRole.Id), role.Id)
            .AddAttribute(nameof(VaultRole.Name), role.Name)
            .AddAttribute(SysName, role.SystemName)
            .AddAttribute(IsSys, role.IsSystemRole)
            .AddAttribute(Descr, role.Description);
}
