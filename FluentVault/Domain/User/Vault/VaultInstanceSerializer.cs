using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultInstanceSerializer : XElementSerializer<VaultInstance>
{
    private const string Vaults = nameof(Vaults);

    public VaultInstanceSerializer(XNamespace @namespace) : base(Vaults, @namespace) { }

    internal override VaultInstance Deserialize(XElement element)
        => new(element.ParseAttributeValue(nameof(VaultInstance.Id), VaultInstanceId.Parse),
            element.GetAttributeValue(nameof(VaultInstance.Name)),
            element.ParseAttributeValue(nameof(VaultInstance.CreateDate), DateTime.Parse),
            element.ParseAttributeValue(nameof(VaultInstance.CreateUserId), VaultUserId.Parse));

    internal override XElement Serialize(VaultInstance instance)
        => BaseElement
            .AddAttribute(nameof(VaultInstance.Id), instance.Id)
            .AddAttribute(nameof(VaultInstance.Name), instance.Name)
            .AddAttribute(nameof(VaultInstance.CreateDate), instance.CreateDate)
            .AddAttribute(nameof(VaultInstance.CreateUserId), instance.CreateUserId);
}
