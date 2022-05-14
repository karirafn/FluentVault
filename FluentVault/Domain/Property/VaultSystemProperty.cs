
using Ardalis.SmartEnum;

namespace FluentVault;

public class VaultSystemProperty : SmartEnum<VaultSystemProperty, VaultPropertyDefinitionId>
{
    // Boolean
    public static readonly VaultSystemProperty LinkedToItem = new(nameof(LinkedToItem), new(13));
    public static readonly VaultSystemProperty ItemAssignable = new(nameof(ItemAssignable), new(14));
    public static readonly VaultSystemProperty Hidden = new(nameof(Hidden), new(19));
    public static readonly VaultSystemProperty LatestVersion = new(nameof(LatestVersion), new(20));
    public static readonly VaultSystemProperty ControlledByChangeOrder = new(nameof(ControlledByChangeOrder), new(21));
    public static readonly VaultSystemProperty HasDrawing = new(nameof(HasDrawing), new(34));
    public static readonly VaultSystemProperty HasParentRelationship = new(nameof(HasParentRelationship), new(35));
    public static readonly VaultSystemProperty LatestReleasedRevision = new(nameof(LatestReleasedRevision), new(38));
    public static readonly VaultSystemProperty ReleasedRevision = new(nameof(ReleasedRevision), new(39));
    public static readonly VaultSystemProperty Obsolete = new(nameof(Obsolete), new(42));
    public static readonly VaultSystemProperty FileReplicated = new(nameof(FileReplicated), new(56));
    public static readonly VaultSystemProperty HasModelState = new(nameof(HasModelState), new(237));
    public static readonly VaultSystemProperty IsTableDriven = new(nameof(IsTableDriven), new(238));
    public static readonly VaultSystemProperty IsTrueModelState = new(nameof(IsTrueModelState), new(239));

    // DateTime
    public static readonly VaultSystemProperty DateVersionCreated = new(nameof(DateVersionCreated), new(5));
    public static readonly VaultSystemProperty CreateDate = new(nameof(CreateDate), new(7));
    public static readonly VaultSystemProperty CheckedIn = new(nameof(CheckedIn), new(8));
    public static readonly VaultSystemProperty DateModified = new(nameof(DateModified), new(11));
    public static readonly VaultSystemProperty CheckedOut = new(nameof(CheckedOut), new(17));
    public static readonly VaultSystemProperty OriginalCreateDate = new(nameof(OriginalCreateDate), new(25));
    public static readonly VaultSystemProperty InitialReleaseDate = new(nameof(InitialReleaseDate), new(40));
    public static readonly VaultSystemProperty DueDate = new(nameof(DueDate), new(65));
    public static readonly VaultSystemProperty DateSubmitted = new(nameof(DateSubmitted), new(67));
    public static readonly VaultSystemProperty DateCreated = new(nameof(DateCreated), new(68));
    public static readonly VaultSystemProperty Created = new(nameof(Created), new(73));
    public static readonly VaultSystemProperty LatestReleasedDate = new(nameof(LatestReleasedDate), new(240));

    // Numeric
    public static readonly VaultSystemProperty Version = new(nameof(Version), new(2));
    public static readonly VaultSystemProperty NumberOfAttachments = new(nameof(NumberOfAttachments), new(4));
    public static readonly VaultSystemProperty FileSize = new(nameof(FileSize), new(12));
    public static readonly VaultSystemProperty PropertyCompliance = new(nameof(PropertyCompliance), new(36));
    public static readonly VaultSystemProperty PropertyComplianceHistorical = new(nameof(PropertyComplianceHistorical), new(37));
    public static readonly VaultSystemProperty CategoryGlyph = new(nameof(CategoryGlyph), new(45));
    public static readonly VaultSystemProperty CategoryGlyphHistorical = new(nameof(CategoryGlyphHistorical), new(46));
    public static readonly VaultSystemProperty NumberOfFileAttachments = new(nameof(NumberOfFileAttachments), new(69));
    public static readonly VaultSystemProperty NumberOfItems = new(nameof(NumberOfItems), new(70));
    public static readonly VaultSystemProperty FileLinkState = new(nameof(FileLinkState), new(76));

    // String
    public static readonly VaultSystemProperty Classification = new(nameof(Classification), new(1));
    public static readonly VaultSystemProperty Comment = new(nameof(Comment), new(3));
    public static readonly VaultSystemProperty CreatedBy = new(nameof(CreatedBy), new(6));
    public static readonly VaultSystemProperty FileName = new(nameof(FileName), new(9));
    public static readonly VaultSystemProperty FileNameHistorical = new(nameof(FileNameHistorical), new(10));
    public static readonly VaultSystemProperty CheckedOutLocalSpec = new(nameof(CheckedOutLocalSpec), new(15));
    public static readonly VaultSystemProperty CheckedOutMachine = new(nameof(CheckedOutMachine), new(16));
    public static readonly VaultSystemProperty CheckedOutBy = new(nameof(CheckedOutBy), new(18));
    public static readonly VaultSystemProperty ChangeOrderState = new(nameof(ChangeOrderState), new(22));
    public static readonly VaultSystemProperty VisualizationAttachment = new(nameof(VisualizationAttachment), new(23));
    public static readonly VaultSystemProperty Originator = new(nameof(Originator), new(24));
    public static readonly VaultSystemProperty Provider = new(nameof(Provider), new(27));
    public static readonly VaultSystemProperty iLogicRuleStatus = new(nameof(iLogicRuleStatus), new(28));
    public static readonly VaultSystemProperty FolderPath = new(nameof(FolderPath), new(29));
    public static new readonly VaultSystemProperty Name = new(nameof(Name), new(30));
    public static readonly VaultSystemProperty TargetEntityClass = new(nameof(TargetEntityClass), new(31));
    public static readonly VaultSystemProperty ParentEntityClass = new(nameof(ParentEntityClass), new(32));
    public static readonly VaultSystemProperty FileExtension = new(nameof(FileExtension), new(33));
    public static readonly VaultSystemProperty InitialApprover = new(nameof(InitialApprover), new(41));
    public static readonly VaultSystemProperty CategoryName = new(nameof(CategoryName), new(43));
    public static readonly VaultSystemProperty CategoryNameHistorical = new(nameof(CategoryNameHistorical), new(44));
    public static readonly VaultSystemProperty LifecycleDefinition = new(nameof(LifecycleDefinition), new(47));
    public static readonly VaultSystemProperty LifecycleDefinitionHistorical = new(nameof(LifecycleDefinitionHistorical), new(48));
    public static readonly VaultSystemProperty State = new(nameof(State), new(49));
    public static readonly VaultSystemProperty StateHistorical = new(nameof(StateHistorical), new(50));
    public static readonly VaultSystemProperty RevisionScheme = new(nameof(RevisionScheme), new(51));
    public static readonly VaultSystemProperty RevisionSchemeHistorical = new(nameof(RevisionSchemeHistorical), new(52));
    public static readonly VaultSystemProperty Revision = new(nameof(Revision), new(53));
    public static readonly VaultSystemProperty LastUpdatedBy = new(nameof(LastUpdatedBy), new(57));
    public static readonly VaultSystemProperty Number = new(nameof(Number), new(58));
    public static readonly VaultSystemProperty TitleItemCO = new(nameof(TitleItemCO), new(59));
    public static readonly VaultSystemProperty Units = new(nameof(Units), new(60));
    public static readonly VaultSystemProperty DescriptionItemCO = new(nameof(DescriptionItemCO), new(61));
    public static readonly VaultSystemProperty Installation = new(nameof(Installation), new(62));
    public static readonly VaultSystemProperty Location = new(nameof(Location), new(63));
    public static readonly VaultSystemProperty Tag = new(nameof(Tag), new(64));
    public static readonly VaultSystemProperty SubmittedBy = new(nameof(SubmittedBy), new(66));
    public static readonly VaultSystemProperty SubjectCO = new(nameof(SubjectCO), new(71));
    public static readonly VaultSystemProperty Message = new(nameof(Message), new(72));
    public static readonly VaultSystemProperty CustomObjectName = new(nameof(CustomObjectName), new(74));
    public static readonly VaultSystemProperty CustomObjectSystemName = new(nameof(CustomObjectSystemName), new(75));
    public static readonly VaultSystemProperty LatestApprover = new(nameof(LatestApprover), new(241));

    protected VaultSystemProperty(string name, VaultPropertyDefinitionId value) : base(name, value) { }

    public static implicit operator VaultSystemProperty(VaultPropertyDefinitionId id) => id;
}
