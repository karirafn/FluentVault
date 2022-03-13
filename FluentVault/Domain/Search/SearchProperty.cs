
using Ardalis.SmartEnum;

namespace FluentVault;

public abstract class SearchProperty : SmartEnum<SearchProperty, PropertyId>
{
    protected SearchProperty(string name, PropertyId value) : base(name, value) { }
}

public sealed class BooleanSearchProperty : SearchProperty
{
    public static readonly BooleanSearchProperty LinkedToItem = new(nameof(LinkedToItem), new(13));
    public static readonly BooleanSearchProperty ItemAssignable = new(nameof(ItemAssignable), new(14));
    public static readonly BooleanSearchProperty Hidden = new(nameof(Hidden), new(19));
    public static readonly BooleanSearchProperty LatestVersion = new(nameof(LatestVersion), new(20));
    public static readonly BooleanSearchProperty ControlledByChangeOrder = new(nameof(ControlledByChangeOrder), new(21));
    public static readonly BooleanSearchProperty HasDrawing = new(nameof(HasDrawing), new(34));
    public static readonly BooleanSearchProperty HasParentRelationship = new(nameof(HasParentRelationship), new(35));
    public static readonly BooleanSearchProperty LatestReleasedRevision = new(nameof(LatestReleasedRevision), new(38));
    public static readonly BooleanSearchProperty ReleasedRevision = new(nameof(ReleasedRevision), new(39));
    public static readonly BooleanSearchProperty Obsolete = new(nameof(Obsolete), new(42));
    public static readonly BooleanSearchProperty FileReplicated = new(nameof(FileReplicated), new(56));
    public static readonly BooleanSearchProperty HasModelState = new(nameof(HasModelState), new(237));
    public static readonly BooleanSearchProperty IsTableDriven = new(nameof(IsTableDriven), new(238));
    public static readonly BooleanSearchProperty IsTrueModelState = new(nameof(IsTrueModelState), new(239));

    public BooleanSearchProperty(string name, PropertyId value) : base(name, value) { }
}

public sealed class DateTimeSearchProperty : SearchProperty
{
    public static readonly DateTimeSearchProperty DateVersionCreated = new(nameof(DateVersionCreated), new(5));
    public static readonly DateTimeSearchProperty CreateDate = new(nameof(CreateDate), new(7));
    public static readonly DateTimeSearchProperty CheckedIn = new(nameof(CheckedIn), new(8));
    public static readonly DateTimeSearchProperty DateModified = new(nameof(DateModified), new(11));
    public static readonly DateTimeSearchProperty CheckedOut = new(nameof(CheckedOut), new(17));
    public static readonly DateTimeSearchProperty OriginalCreateDate = new(nameof(OriginalCreateDate), new(25));
    public static readonly DateTimeSearchProperty InitialReleaseDate = new(nameof(InitialReleaseDate), new(40));
    public static readonly DateTimeSearchProperty DueDate = new(nameof(DueDate), new(65));
    public static readonly DateTimeSearchProperty DateSubmitted = new(nameof(DateSubmitted), new(67));
    public static readonly DateTimeSearchProperty DateCreated = new(nameof(DateCreated), new(68));
    public static readonly DateTimeSearchProperty Created = new(nameof(Created), new(73));
    public static readonly DateTimeSearchProperty LatestReleasedDate = new(nameof(LatestReleasedDate), new(240));

    public DateTimeSearchProperty(string name, PropertyId value) : base(name, value) { }
}

public sealed class NumericSearchProperty : SearchProperty
{
    public static readonly NumericSearchProperty Version = new(nameof(Version), new(2));
    public static readonly NumericSearchProperty NumberOfAttachments = new(nameof(NumberOfAttachments), new(4));
    public static readonly NumericSearchProperty FileSize = new(nameof(FileSize), new(12));
    public static readonly NumericSearchProperty PropertyCompliance = new(nameof(PropertyCompliance), new(36));
    public static readonly NumericSearchProperty PropertyComplianceHistorical = new(nameof(PropertyComplianceHistorical), new(37));
    public static readonly NumericSearchProperty CategoryGlyph = new(nameof(CategoryGlyph), new(45));
    public static readonly NumericSearchProperty CategoryGlyphHistorical = new(nameof(CategoryGlyphHistorical), new(46));
    public static readonly NumericSearchProperty NumberOfFileAttachments = new(nameof(NumberOfFileAttachments), new(69));
    public static readonly NumericSearchProperty NumberOfItems = new(nameof(NumberOfItems), new(70));
    public static readonly NumericSearchProperty FileLinkState = new(nameof(FileLinkState), new(76));

    public NumericSearchProperty(string name, PropertyId value) : base(name, value) { }
}

public sealed class StringSearchProperty : SearchProperty
{
    public static readonly StringSearchProperty Classification = new(nameof(Classification), new(1));
    public static readonly StringSearchProperty Comment = new(nameof(Comment), new(3));
    public static readonly StringSearchProperty CreatedBy = new(nameof(CreatedBy), new(6));
    public static readonly StringSearchProperty FileName = new(nameof(FileName), new(9));
    public static readonly StringSearchProperty FileNameHistorical = new(nameof(FileNameHistorical), new(10));
    public static readonly StringSearchProperty CheckedOutLocalSpec = new(nameof(CheckedOutLocalSpec), new(15));
    public static readonly StringSearchProperty CheckedOutMachine = new(nameof(CheckedOutMachine), new(16));
    public static readonly StringSearchProperty CheckedOutBy = new(nameof(CheckedOutBy), new(18));
    public static readonly StringSearchProperty ChangeOrderState = new(nameof(ChangeOrderState), new(22));
    public static readonly StringSearchProperty VisualizationAttachment = new(nameof(VisualizationAttachment), new(23));
    public static readonly StringSearchProperty Originator = new(nameof(Originator), new(24));
    public static readonly StringSearchProperty Provider = new(nameof(Provider), new(27));
    public static readonly StringSearchProperty iLogicRuleStatus = new(nameof(iLogicRuleStatus), new(28));
    public static readonly StringSearchProperty FolderPath = new(nameof(FolderPath), new(29));
    public static readonly StringSearchProperty Name = new(nameof(Name), new(30));
    public static readonly StringSearchProperty TargetEntityClass = new(nameof(TargetEntityClass), new(31));
    public static readonly StringSearchProperty ParentEntityClass = new(nameof(ParentEntityClass), new(32));
    public static readonly StringSearchProperty FileExtension = new(nameof(FileExtension), new(33));
    public static readonly StringSearchProperty InitialApprover = new(nameof(InitialApprover), new(41));
    public static readonly StringSearchProperty CategoryName = new(nameof(CategoryName), new(43));
    public static readonly StringSearchProperty CategoryNameHistorical = new(nameof(CategoryNameHistorical), new(44));
    public static readonly StringSearchProperty LifecycleDefinition = new(nameof(LifecycleDefinition), new(47));
    public static readonly StringSearchProperty LifecycleDefinitionHistorical = new(nameof(LifecycleDefinitionHistorical), new(48));
    public static readonly StringSearchProperty State = new(nameof(State), new(49));
    public static readonly StringSearchProperty StateHistorical = new(nameof(StateHistorical), new(50));
    public static readonly StringSearchProperty RevisionScheme = new(nameof(RevisionScheme), new(51));
    public static readonly StringSearchProperty RevisionSchemeHistorical = new(nameof(RevisionSchemeHistorical), new(52));
    public static readonly StringSearchProperty Revision = new(nameof(Revision), new(53));
    public static readonly StringSearchProperty LastUpdatedBy = new(nameof(LastUpdatedBy), new(57));
    public static readonly StringSearchProperty Number = new(nameof(Number), new(58));
    public static readonly StringSearchProperty TitleItemCO = new(nameof(TitleItemCO), new(59));
    public static readonly StringSearchProperty Units = new(nameof(Units), new(60));
    public static readonly StringSearchProperty DescriptionItemCO = new(nameof(DescriptionItemCO), new(61));
    public static readonly StringSearchProperty Installation = new(nameof(Installation), new(62));
    public static readonly StringSearchProperty Location = new(nameof(Location), new(63));
    public static readonly StringSearchProperty Tag = new(nameof(Tag), new(64));
    public static readonly StringSearchProperty SubmittedBy = new(nameof(SubmittedBy), new(66));
    public static readonly StringSearchProperty SubjectCO = new(nameof(SubjectCO), new(71));
    public static readonly StringSearchProperty Message = new(nameof(Message), new(72));
    public static readonly StringSearchProperty CustomObjectName = new(nameof(CustomObjectName), new(74));
    public static readonly StringSearchProperty CustomObjectSystemName = new(nameof(CustomObjectSystemName), new(75));
    public static readonly StringSearchProperty LatestApprover = new(nameof(LatestApprover), new(241));

    public StringSearchProperty(string name, PropertyId value) : base(name, value) { }
}
