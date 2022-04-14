using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultUserInfoSerializer : XElementSerializer<VaultUserInfo>
{
    private const string UserInfo = nameof(UserInfo);

    private readonly VaultUserSerializer _userSerializer;
    private readonly VaultRoleSerializer _roleSerializer;
    private readonly VaultInstanceSerializer _instanceSerializer;
    private readonly VaultGroupSerializer _groupSerializer;

    public VaultUserInfoSerializer(XNamespace @namespace) : base(UserInfo, @namespace)
    {
        _userSerializer = new(Namespace);
        _roleSerializer = new(Namespace);
        _instanceSerializer = new(Namespace);
        _groupSerializer = new(Namespace);
    }

    internal override VaultUserInfo Deserialize(XElement element)
        => new(_userSerializer.Deserialize(element),
            _roleSerializer.DeserializeMany(element),
            _instanceSerializer.DeserializeMany(element),
            _groupSerializer.DeserializeMany(element));

    internal override XElement Serialize(VaultUserInfo userInfo)
        => BaseElement
            .AddElement(_userSerializer.Serialize(userInfo.User))
            .AddElements(userInfo.Roles.Select(role => _roleSerializer.Serialize(role)))
            .AddElements(userInfo.Vaults.Select(vault => _instanceSerializer.Serialize(vault)))
            .AddElements(userInfo.Groups.Select(group => _groupSerializer.Serialize(group)));
}
