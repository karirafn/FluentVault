using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultUserSerializer : XElementSerializer<VaultUser>
{
    private const string User = nameof(User);
    private const string IsSys = nameof(IsSys);
    private const string Auth = nameof(Auth);

    public VaultUserSerializer(XNamespace @namespace) : base(User, @namespace) { }

    internal override VaultUser Deserialize(XElement element)
    {
        element = GetSerializationElement(element);

        return new(element.ParseAttributeValue(nameof(VaultUser.Id), VaultUserId.Parse),
                   element.GetAttributeValue(nameof(VaultUser.Name)),
                   element.GetAttributeValue(nameof(VaultUser.FirstName)),
                   element.GetAttributeValue(nameof(VaultUser.LastName)),
                   element.GetAttributeValue(nameof(VaultUser.Email)),
                   element.ParseAttributeValue(nameof(VaultUser.CreateUserId), VaultUserId.Parse),
                   element.ParseAttributeValue(nameof(VaultUser.CreateDate), DateTime.Parse),
                   element.ParseAttributeValue(nameof(VaultUser.IsActive), bool.Parse),
                   element.ParseAttributeValue(IsSys, bool.Parse),
                   element.ParseAttributeValue(Auth, x => VaultAuthenticationType.FromName(x)));
    }

    internal override XElement Serialize(VaultUser user)
        => BaseElement
            .AddAttribute(nameof(VaultUser.Id), user.Id)
            .AddAttribute(nameof(VaultUser.Name), user.Name)
            .AddAttribute(nameof(VaultUser.FirstName), user.FirstName)
            .AddAttribute(nameof(VaultUser.LastName), user.LastName)
            .AddAttribute(nameof(VaultUser.Email), user.Email)
            .AddAttribute(nameof(VaultUser.CreateUserId), user.CreateUserId)
            .AddAttribute(nameof(VaultUser.CreateDate), user.CreateDate)
            .AddAttribute(nameof(VaultUser.IsActive), user.IsActive)
            .AddAttribute(IsSys, user.IsSystemUser)
            .AddAttribute(Auth, user.AuthenticationType);
}
