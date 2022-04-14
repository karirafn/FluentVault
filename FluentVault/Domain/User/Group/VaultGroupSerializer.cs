using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultGroupSerializer : XElementSerializer<VaultGroup>
{
    private const string Groups = nameof(Groups);
    private const string EmailDL = nameof(EmailDL);
    private const string IsActive = nameof(IsActive);
    private const string IsSys = nameof(IsSys);
    private const string Auth = nameof(Auth);

    public VaultGroupSerializer(XNamespace @namespace) : base(Groups, @namespace) { }

    internal override VaultGroup Deserialize(XElement element)
        => new(element.ParseAttributeValue(nameof(VaultGroup.Id), VaultGroupId.Parse),
            element.GetAttributeValue(nameof(VaultGroup.Name)),
            element.GetAttributeValue(EmailDL),
            element.ParseAttributeValue(nameof(VaultGroup.CreateUserId), VaultUserId.Parse),
            element.ParseAttributeValue(nameof(VaultGroup.CreateDate), DateTime.Parse),
            element.ParseAttributeValue(IsActive, bool.Parse),
            element.ParseAttributeValue(IsSys, bool.Parse),
            element.ParseAttributeValue(Auth, x => VaultAuthenticationType.FromName(x)));

    internal override XElement Serialize(VaultGroup group)
        => BaseElement
            .AddAttribute(nameof(VaultGroup.Id), group.Id)
            .AddAttribute(nameof(VaultGroup.Name), group.Name)
            .AddAttribute(EmailDL, group.Email)
            .AddAttribute(nameof(VaultGroup.CreateUserId), group.CreateUserId)
            .AddAttribute(nameof(VaultGroup.CreateDate), group.CreateDate)
            .AddAttribute(IsActive, group.IsActive)
            .AddAttribute(IsSys, group.IsSystemGroup)
            .AddAttribute(Auth, group.AuthenticationType);
}
