
using Ardalis.SmartEnum;

namespace FluentVault;

public abstract class SearchProperty : SmartEnum<SearchProperty>
{
    protected SearchProperty(string name, int value) : base(name, value) { }
}

public sealed class BooleanSearchProperty : SearchProperty
{
    public static readonly BooleanSearchProperty LinkedToItem = new(nameof(LinkedToItem), 13);
    public static readonly BooleanSearchProperty ItemAssignable = new(nameof(ItemAssignable), 14);
    public static readonly BooleanSearchProperty Hidden = new(nameof(Hidden), 19);
    public static readonly BooleanSearchProperty LatestVersion = new(nameof(LatestVersion), 20);
    public static readonly BooleanSearchProperty ControlledByChangeOrder = new(nameof(ControlledByChangeOrder), 21);
    public static readonly BooleanSearchProperty HasDrawing = new(nameof(HasDrawing), 34);
    public static readonly BooleanSearchProperty HasParentRelationship = new(nameof(HasParentRelationship), 35);
    public static readonly BooleanSearchProperty LatestReleasedRevision = new(nameof(LatestReleasedRevision), 38);
    public static readonly BooleanSearchProperty ReleasedRevision = new(nameof(ReleasedRevision), 39);
    public static readonly BooleanSearchProperty Obsolete = new(nameof(Obsolete), 42);
    public static readonly BooleanSearchProperty FileReplicated = new(nameof(FileReplicated), 56);
    public static readonly BooleanSearchProperty HasModelState = new(nameof(HasModelState), 237);
    public static readonly BooleanSearchProperty IsTableDriven = new(nameof(IsTableDriven), 238);
    public static readonly BooleanSearchProperty IsTrueModelState = new(nameof(IsTrueModelState), 239);

    public BooleanSearchProperty(string name, int value) : base(name, value) { }
}

public sealed class DateTimeSearchProperty : SearchProperty
{
    public static readonly DateTimeSearchProperty DateVersionCreated = new(nameof(DateVersionCreated), 5);
    public static readonly DateTimeSearchProperty CreateDate = new(nameof(CreateDate), 7);
    public static readonly DateTimeSearchProperty CheckedIn = new(nameof(CheckedIn), 8);
    public static readonly DateTimeSearchProperty DateModified = new(nameof(DateModified), 11);
    public static readonly DateTimeSearchProperty CheckedOut = new(nameof(CheckedOut), 17);
    public static readonly DateTimeSearchProperty OriginalCreateDate = new(nameof(OriginalCreateDate), 25);
    public static readonly DateTimeSearchProperty InitialReleaseDate = new(nameof(InitialReleaseDate), 40);
    public static readonly DateTimeSearchProperty DueDate = new(nameof(DueDate), 65);
    public static readonly DateTimeSearchProperty DateSubmitted = new(nameof(DateSubmitted), 67);
    public static readonly DateTimeSearchProperty DateCreated = new(nameof(DateCreated), 68);
    public static readonly DateTimeSearchProperty Created = new(nameof(Created), 73);
    public static readonly DateTimeSearchProperty LatestReleasedDate = new(nameof(LatestReleasedDate), 240);

    public DateTimeSearchProperty(string name, int value) : base(name, value) { }
}

public sealed class NumericSearchProperty : SearchProperty
{
    public static readonly NumericSearchProperty Version = new(nameof(Version), 2);
    public static readonly NumericSearchProperty NumberOfAttachments = new(nameof(NumberOfAttachments), 4);
    public static readonly NumericSearchProperty FileSize = new(nameof(FileSize), 12);
    public static readonly NumericSearchProperty PropertyCompliance = new(nameof(PropertyCompliance), 36);
    public static readonly NumericSearchProperty PropertyComplianceHistorical = new(nameof(PropertyComplianceHistorical), 37);
    public static readonly NumericSearchProperty CategoryGlyph = new(nameof(CategoryGlyph), 45);
    public static readonly NumericSearchProperty CategoryGlyphHistorical = new(nameof(CategoryGlyphHistorical), 46);
    public static readonly NumericSearchProperty NumberOfFileAttachments = new(nameof(NumberOfFileAttachments), 69);
    public static readonly NumericSearchProperty NumberOfItems = new(nameof(NumberOfItems), 70);
    public static readonly NumericSearchProperty FileLinkState = new(nameof(FileLinkState), 76);

    public NumericSearchProperty(string name, int value) : base(name, value) { }
}

public sealed class StringSearchProperty : SearchProperty
{
    public static readonly StringSearchProperty Classification = new(nameof(Classification), 1);
    public static readonly StringSearchProperty Comment = new(nameof(Comment), 3);
    public static readonly StringSearchProperty CreatedBy = new(nameof(CreatedBy), 6);
    public static readonly StringSearchProperty FileName = new(nameof(FileName), 9);
    public static readonly StringSearchProperty FileNameHistorical = new(nameof(FileNameHistorical), 10);
    public static readonly StringSearchProperty CheckedOutLocalSpec = new(nameof(CheckedOutLocalSpec), 15);
    public static readonly StringSearchProperty CheckedOutMachine = new(nameof(CheckedOutMachine), 16);
    public static readonly StringSearchProperty CheckedOutBy = new(nameof(CheckedOutBy), 18);
    public static readonly StringSearchProperty ChangeOrderState = new(nameof(ChangeOrderState), 22);
    public static readonly StringSearchProperty VisualizationAttachment = new(nameof(VisualizationAttachment), 23);
    public static readonly StringSearchProperty Originator = new(nameof(Originator), 24);
    public static readonly StringSearchProperty Provider = new(nameof(Provider), 27);
    public static readonly StringSearchProperty iLogicRuleStatus = new(nameof(iLogicRuleStatus), 28);
    public static readonly StringSearchProperty FolderPath = new(nameof(FolderPath), 29);
    public static readonly StringSearchProperty Name = new(nameof(Name), 30);
    public static readonly StringSearchProperty TargetEntityClass = new(nameof(TargetEntityClass), 31);
    public static readonly StringSearchProperty ParentEntityClass = new(nameof(ParentEntityClass), 32);
    public static readonly StringSearchProperty FileExtension = new(nameof(FileExtension), 33);
    public static readonly StringSearchProperty InitialApprover = new(nameof(InitialApprover), 41);
    public static readonly StringSearchProperty CategoryName = new(nameof(CategoryName), 43);
    public static readonly StringSearchProperty CategoryNameHistorical = new(nameof(CategoryNameHistorical), 44);
    public static readonly StringSearchProperty LifecycleDefinition = new(nameof(LifecycleDefinition), 47);
    public static readonly StringSearchProperty LifecycleDefinitionHistorical = new(nameof(LifecycleDefinitionHistorical), 48);
    public static readonly StringSearchProperty State = new(nameof(State), 49);
    public static readonly StringSearchProperty StateHistorical = new(nameof(StateHistorical), 50);
    public static readonly StringSearchProperty RevisionScheme = new(nameof(RevisionScheme), 51);
    public static readonly StringSearchProperty RevisionSchemeHistorical = new(nameof(RevisionSchemeHistorical), 52);
    public static readonly StringSearchProperty Revision = new(nameof(Revision), 53);
    public static readonly StringSearchProperty LastUpdatedBy = new(nameof(LastUpdatedBy), 57);
    public static readonly StringSearchProperty Number = new(nameof(Number), 58);
    public static readonly StringSearchProperty TitleItemCO = new(nameof(TitleItemCO), 59);
    public static readonly StringSearchProperty Units = new(nameof(Units), 60);
    public static readonly StringSearchProperty DescriptionItemCO = new(nameof(DescriptionItemCO), 61);
    public static readonly StringSearchProperty Installation = new(nameof(Installation), 62);
    public static readonly StringSearchProperty Location = new(nameof(Location), 63);
    public static readonly StringSearchProperty Tag = new(nameof(Tag), 64);
    public static readonly StringSearchProperty SubmittedBy = new(nameof(SubmittedBy), 66);
    public static readonly StringSearchProperty SubjectCO = new(nameof(SubjectCO), 71);
    public static readonly StringSearchProperty Message = new(nameof(Message), 72);
    public static readonly StringSearchProperty CustomObjectName = new(nameof(CustomObjectName), 74);
    public static readonly StringSearchProperty CustomObjectSystemName = new(nameof(CustomObjectSystemName), 75);
    public static readonly StringSearchProperty LatestApprover = new(nameof(LatestApprover), 241);

    public StringSearchProperty(string name, int value) : base(name, value) { }
}
