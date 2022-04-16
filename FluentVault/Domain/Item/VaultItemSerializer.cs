using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.Entity;
using FluentVault.Extensions;

namespace FluentVault.Domain.Item;
internal class VaultItemSerializer : XElementSerializer<VaultItem>
{
    private const string Item = nameof(Item);
    private const string RevId = nameof(RevId);
    private const string LfCycStateId = nameof(LfCycStateId);
    private const string NumSchmId = nameof(NumSchmId);
    private const string MaxCommittedId = nameof(MaxCommittedId);
    private const string CadBOMStruct = nameof(CadBOMStruct);
    private const string LastModUserId = nameof(LastModUserId);
    private const string LastModUserName = nameof(LastModUserName);
    private const string LastModDate = nameof(LastModDate);
    private const string ItemNum = nameof(ItemNum);
    private const string Detail = nameof(Detail);
    private const string VerNum = nameof(VerNum);
    private const string Comm = nameof(Comm);
    private const string ControlledByChangeOrder = nameof(ControlledByChangeOrder);
    private const string Locked = nameof(Locked);

    private readonly VaultEntityLifeCycleSerializer _lifeCycleSerializer;
    private readonly VaultEntityCategorySerializer _categorySerializer;

    public VaultItemSerializer(XNamespace @namespace) : base(Item, @namespace)
    {
        _lifeCycleSerializer = new(Namespace);
        _categorySerializer = new(Namespace);
    }

    internal override XElement Serialize(VaultItem item)
        => BaseElement
            .AddAttribute(nameof(VaultItem.Id), item.Id)
            .AddAttribute(nameof(VaultItem.MasterId), item.MasterId)
            .AddAttribute(RevId, item.RevisionId)
            .AddAttribute(LfCycStateId, item.StateId)
            .AddAttribute(NumSchmId, item.NumberingSchemeId)
            .AddAttribute(MaxCommittedId, item.MaximumCommittedId)
            .AddAttribute(CadBOMStruct, item.BomStructure)
            .AddAttribute(LastModUserId, item.LastModifiedUserId)
            .AddAttribute(LastModUserName, item.LastModifiedUserName)
            .AddAttribute(LastModDate, item.LastModifiedDate)
            .AddAttribute(ItemNum, item.ItemNumber)
            .AddAttribute(nameof(VaultItem.Title), item.Title)
            .AddAttribute(Detail, item.Description)
            .AddAttribute(VerNum, item.Version)
            .AddAttribute(Comm, item.Comment)
            .AddAttribute(nameof(VaultItem.UnitId), item.UnitId)
            .AddAttribute(nameof(VaultItem.Units), item.Units)
            .AddAttribute(ControlledByChangeOrder, item.IsControlledByChangeOrder)
            .AddAttribute(nameof(VaultItem.IsCloaked), item.IsCloaked)
            .AddAttribute(Locked, item.IsLocked)
            .AddElement(_categorySerializer.Serialize(item.Category))
            .AddElement(_lifeCycleSerializer.Serialize(item.LifeCycle));


    internal override VaultItem Deserialize(XElement element)
        => new(element.ParseAttributeValue(nameof(VaultItem.Id), VaultItemId.Parse),
            element.ParseAttributeValue(nameof(VaultItem.MasterId), VaultMasterId.Parse),
            element.ParseAttributeValue(RevId, VaultRevisionId.Parse),
            element.ParseAttributeValue(LfCycStateId, VaultLifeCycleStateId.Parse),
            element.ParseAttributeValue(NumSchmId, VaultNumberingSchemeId.Parse),
            element.ParseAttributeValue(MaxCommittedId, VaultFileIterationId.Parse),
            element.ParseAttributeValue(CadBOMStruct, x => VaultBomStructure.FromName(x)),
            element.ParseAttributeValue(LastModUserId, VaultUserId.Parse),
            element.GetAttributeValue(LastModUserName),
            element.ParseAttributeValue(LastModDate, DateTime.Parse),
            element.GetAttributeValue(ItemNum),
            element.GetAttributeValue(nameof(VaultItem.Title)),
            element.GetAttributeValue(Detail),
            element.ParseAttributeValue(VerNum, long.Parse),
            element.GetAttributeValue(Comm),
            element.ParseAttributeValue(nameof(VaultItem.UnitId), VaultUnitId.Parse),
            element.GetAttributeValue(nameof(VaultItem.Units)),
            element.ParseAttributeValue(ControlledByChangeOrder, bool.Parse),
            element.ParseAttributeValue(nameof(VaultItem.IsCloaked), bool.Parse),
            element.ParseAttributeValue(Locked, bool.Parse),
            _categorySerializer.Deserialize(element),
            _lifeCycleSerializer.Deserialize(element));
}
