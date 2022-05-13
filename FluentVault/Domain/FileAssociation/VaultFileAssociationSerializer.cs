using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
internal class VaultFileAssociationSerializer : XElementSerializer<VaultFileAssociation>
{
    private const string FileAssoc = nameof(FileAssoc);
    private const string Typ = nameof(Typ);
    private const string RefId = nameof(RefId);
    private const string VaultPathChanged = nameof(VaultPathChanged);
    private const string CorrectRev = nameof(CorrectRev);
    private const string ParFile = nameof(ParFile);
    private const string CldFile = nameof(CldFile);

    private readonly VaultFileSerializer _parentFileSerializer;
    private readonly VaultFileSerializer _childFileSerializer;

    public VaultFileAssociationSerializer(XNamespace @namespace) : base(FileAssoc, @namespace) 
    {
        _parentFileSerializer = new(ParFile, @namespace);
        _childFileSerializer = new(CldFile, @namespace);
    }

    internal override XElement Serialize(VaultFileAssociation association) =>
        BaseElement
            .AddAttribute(nameof(VaultFileAssociation.Id), association.Id)
            .AddAttribute(Typ, association.Type)
            .AddAttribute(nameof(VaultFileAssociation.Source), association.Source)
            .AddAttribute(RefId, association.ReferenceId)
            .AddAttribute(nameof(VaultFileAssociation.ExpectedVaultPath), association.ExpectedVaultPath)
            .AddAttribute(VaultPathChanged, association.HasVaultPathChanged)
            .AddAttribute(CorrectRev, association.IsCorrectRevision)
            .AddElement(_parentFileSerializer.Serialize(association.Parent))
            .AddElement(_childFileSerializer.Serialize(association.Parent));

    internal override VaultFileAssociation Deserialize(XElement element)
        => new(element.ParseAttributeValue(nameof(VaultFileAssociation.Id), VaultFileAssociationId.Parse),
            element.ParseAttributeValue(Typ, x => VaultFileAssociationType.FromName(x)),
            element.GetAttributeValue(nameof(VaultFileAssociation.Source)),
            element.GetAttributeValue(RefId),
            element.GetAttributeValue(nameof(VaultFileAssociation.ExpectedVaultPath)),
            element.ParseAttributeValue(VaultPathChanged, bool.Parse),
            element.ParseAttributeValue(CorrectRev, bool.Parse),
            _parentFileSerializer.Deserialize(element),
            _childFileSerializer.Deserialize(element));
}
