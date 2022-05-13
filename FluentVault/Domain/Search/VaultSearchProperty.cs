
using Ardalis.SmartEnum;

namespace FluentVault;

public class VaultSearchProperty : SmartEnum<VaultSearchProperty, VaultPropertyDefinitionId>
{
    // Boolean
    public static readonly VaultSearchProperty LinkedToItem = new(nameof(LinkedToItem), new(13));
    public static readonly VaultSearchProperty ItemAssignable = new(nameof(ItemAssignable), new(14));
    public static readonly VaultSearchProperty Hidden = new(nameof(Hidden), new(19));
    public static readonly VaultSearchProperty LatestVersion = new(nameof(LatestVersion), new(20));
    public static readonly VaultSearchProperty ControlledByChangeOrder = new(nameof(ControlledByChangeOrder), new(21));
    public static readonly VaultSearchProperty HasDrawing = new(nameof(HasDrawing), new(34));
    public static readonly VaultSearchProperty HasParentRelationship = new(nameof(HasParentRelationship), new(35));
    public static readonly VaultSearchProperty LatestReleasedRevision = new(nameof(LatestReleasedRevision), new(38));
    public static readonly VaultSearchProperty ReleasedRevision = new(nameof(ReleasedRevision), new(39));
    public static readonly VaultSearchProperty Obsolete = new(nameof(Obsolete), new(42));
    public static readonly VaultSearchProperty FileReplicated = new(nameof(FileReplicated), new(56));
    public static readonly VaultSearchProperty HasModelState = new(nameof(HasModelState), new(237));
    public static readonly VaultSearchProperty IsTableDriven = new(nameof(IsTableDriven), new(238));
    public static readonly VaultSearchProperty IsTrueModelState = new(nameof(IsTrueModelState), new(239));

    // DateTime
    public static readonly VaultSearchProperty DateVersionCreated = new(nameof(DateVersionCreated), new(5));
    public static readonly VaultSearchProperty CreateDate = new(nameof(CreateDate), new(7));
    public static readonly VaultSearchProperty CheckedIn = new(nameof(CheckedIn), new(8));
    public static readonly VaultSearchProperty DateModified = new(nameof(DateModified), new(11));
    public static readonly VaultSearchProperty CheckedOut = new(nameof(CheckedOut), new(17));
    public static readonly VaultSearchProperty OriginalCreateDate = new(nameof(OriginalCreateDate), new(25));
    public static readonly VaultSearchProperty InitialReleaseDate = new(nameof(InitialReleaseDate), new(40));
    public static readonly VaultSearchProperty DueDate = new(nameof(DueDate), new(65));
    public static readonly VaultSearchProperty DateSubmitted = new(nameof(DateSubmitted), new(67));
    public static readonly VaultSearchProperty DateCreated = new(nameof(DateCreated), new(68));
    public static readonly VaultSearchProperty Created = new(nameof(Created), new(73));
    public static readonly VaultSearchProperty LatestReleasedDate = new(nameof(LatestReleasedDate), new(240));

    // Numeric
    public static readonly VaultSearchProperty Version = new(nameof(Version), new(2));
    public static readonly VaultSearchProperty NumberOfAttachments = new(nameof(NumberOfAttachments), new(4));
    public static readonly VaultSearchProperty FileSize = new(nameof(FileSize), new(12));
    public static readonly VaultSearchProperty PropertyCompliance = new(nameof(PropertyCompliance), new(36));
    public static readonly VaultSearchProperty PropertyComplianceHistorical = new(nameof(PropertyComplianceHistorical), new(37));
    public static readonly VaultSearchProperty CategoryGlyph = new(nameof(CategoryGlyph), new(45));
    public static readonly VaultSearchProperty CategoryGlyphHistorical = new(nameof(CategoryGlyphHistorical), new(46));
    public static readonly VaultSearchProperty NumberOfFileAttachments = new(nameof(NumberOfFileAttachments), new(69));
    public static readonly VaultSearchProperty NumberOfItems = new(nameof(NumberOfItems), new(70));
    public static readonly VaultSearchProperty FileLinkState = new(nameof(FileLinkState), new(76));

    // String
    public static readonly VaultSearchProperty Classification = new(nameof(Classification), new(1));
    public static readonly VaultSearchProperty Comment = new(nameof(Comment), new(3));
    public static readonly VaultSearchProperty CreatedBy = new(nameof(CreatedBy), new(6));
    public static readonly VaultSearchProperty FileName = new(nameof(FileName), new(9));
    public static readonly VaultSearchProperty FileNameHistorical = new(nameof(FileNameHistorical), new(10));
    public static readonly VaultSearchProperty CheckedOutLocalSpec = new(nameof(CheckedOutLocalSpec), new(15));
    public static readonly VaultSearchProperty CheckedOutMachine = new(nameof(CheckedOutMachine), new(16));
    public static readonly VaultSearchProperty CheckedOutBy = new(nameof(CheckedOutBy), new(18));
    public static readonly VaultSearchProperty ChangeOrderState = new(nameof(ChangeOrderState), new(22));
    public static readonly VaultSearchProperty VisualizationAttachment = new(nameof(VisualizationAttachment), new(23));
    public static readonly VaultSearchProperty Originator = new(nameof(Originator), new(24));
    public static readonly VaultSearchProperty Provider = new(nameof(Provider), new(27));
    public static readonly VaultSearchProperty iLogicRuleStatus = new(nameof(iLogicRuleStatus), new(28));
    public static readonly VaultSearchProperty FolderPath = new(nameof(FolderPath), new(29));
    public static new readonly VaultSearchProperty Name = new(nameof(Name), new(30));
    public static readonly VaultSearchProperty TargetEntityClass = new(nameof(TargetEntityClass), new(31));
    public static readonly VaultSearchProperty ParentEntityClass = new(nameof(ParentEntityClass), new(32));
    public static readonly VaultSearchProperty FileExtension = new(nameof(FileExtension), new(33));
    public static readonly VaultSearchProperty InitialApprover = new(nameof(InitialApprover), new(41));
    public static readonly VaultSearchProperty CategoryName = new(nameof(CategoryName), new(43));
    public static readonly VaultSearchProperty CategoryNameHistorical = new(nameof(CategoryNameHistorical), new(44));
    public static readonly VaultSearchProperty LifecycleDefinition = new(nameof(LifecycleDefinition), new(47));
    public static readonly VaultSearchProperty LifecycleDefinitionHistorical = new(nameof(LifecycleDefinitionHistorical), new(48));
    public static readonly VaultSearchProperty State = new(nameof(State), new(49));
    public static readonly VaultSearchProperty StateHistorical = new(nameof(StateHistorical), new(50));
    public static readonly VaultSearchProperty RevisionScheme = new(nameof(RevisionScheme), new(51));
    public static readonly VaultSearchProperty RevisionSchemeHistorical = new(nameof(RevisionSchemeHistorical), new(52));
    public static readonly VaultSearchProperty Revision = new(nameof(Revision), new(53));
    public static readonly VaultSearchProperty LastUpdatedBy = new(nameof(LastUpdatedBy), new(57));
    public static readonly VaultSearchProperty Number = new(nameof(Number), new(58));
    public static readonly VaultSearchProperty TitleItemCO = new(nameof(TitleItemCO), new(59));
    public static readonly VaultSearchProperty Units = new(nameof(Units), new(60));
    public static readonly VaultSearchProperty DescriptionItemCO = new(nameof(DescriptionItemCO), new(61));
    public static readonly VaultSearchProperty Installation = new(nameof(Installation), new(62));
    public static readonly VaultSearchProperty Location = new(nameof(Location), new(63));
    public static readonly VaultSearchProperty Tag = new(nameof(Tag), new(64));
    public static readonly VaultSearchProperty SubmittedBy = new(nameof(SubmittedBy), new(66));
    public static readonly VaultSearchProperty SubjectCO = new(nameof(SubjectCO), new(71));
    public static readonly VaultSearchProperty Message = new(nameof(Message), new(72));
    public static readonly VaultSearchProperty CustomObjectName = new(nameof(CustomObjectName), new(74));
    public static readonly VaultSearchProperty CustomObjectSystemName = new(nameof(CustomObjectSystemName), new(75));
    public static readonly VaultSearchProperty LatestApprover = new(nameof(LatestApprover), new(241));

    protected VaultSearchProperty(string name, VaultPropertyDefinitionId value) : base(name, value) { }
}
