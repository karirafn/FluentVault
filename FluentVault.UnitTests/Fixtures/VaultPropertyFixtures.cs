using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoFixture;

namespace FluentVault.UnitTests.Fixtures;

internal static partial class VaultResponseFixtures
{
    public static (string Body, IEnumerable<VaultProperty> Files) GetVaultPropertyFixtures(int count)
    {
        Fixture fixture = new();
        fixture.Register(() => AllowedMappingDirection.Read);
        fixture.Register(() => Classification.Standard);
        fixture.Register(() => ContentSourceDefinitionType.File);
        fixture.Register(() => DataType.String);
        fixture.Register(() => EntityClass.File);
        fixture.Register(() => MappingDirection.Write);
        fixture.Register(() => MappingType.Constant);
        fixture.Register(() => VaultPropertyConstraintType.RequiresValue);

        return CreateBody<VaultProperty>(fixture, count, "GetPropertyDefinitionInfosByEntityClassId", "http://AutodeskDM/Services/Property/1/7/2020/", CreatePropertyBody);
    }

    private static string CreatePropertyBody(VaultProperty property) => $@"<PropDefInfo>
                    <PropDef
						Id=""{property.Definition.Id}""
						Typ=""{property.Definition.DataType}""
						DispName=""{property.Definition.DisplayName}""
						SysName=""{property.Definition.SystemName}""
						IsAct=""{property.Definition.IsActive}""
						IsBasicSrch=""{property.Definition.IsUsedInBasicSearch}""
						IsSys=""{property.Definition.IsSystemProperty}""
						UsageCount=""{property.Definition.UsageCount}"">
						<EntClassAssocArray>
                            {property.Definition.EntityClassAssociations.Aggregate(new StringBuilder(),
                                (builder, association) => builder.Append(CreateEntityClassAssociationBody(association)))};
						</EntClassAssocArray>
					</PropDef>
                    <ListValArray>
                        {property.ListValues.Aggregate(new StringBuilder(),
                            (builder, value) => builder.Append(CreateListValueBody(value)))}
                    </ListValArray>
                    <PropConstrArray>
                        {property.Constraints.Aggregate(new StringBuilder(),
                            (builder, constraint) => builder.Append(CreatePropertyConstraintBody(constraint)))}
                    </PropConstrArray>
                    <CtntSrcPropDefArray>
                        {property.EntityClassContentSourcePropertyDefinitions.Aggregate(new StringBuilder(),
                            (builder, definition) => builder.Append(CreateEntityClassContentSourcePropertyDefinitionBosy(definition)))}
                    </CtntSrcPropDefArray>
                </PropDefInfo>";

    private static string CreateEntityClassAssociationBody(EntityClassAssociation association)
        => $@"<EntClassAssoc EntClassId=""{association.EntityClass}"" MapDirection=""{association.AllowedMappingDirection}""/>";

    private static string CreatePropertyConstraintBody(VaultPropertyConstraint constraint)
        => $@"<PropertyConstraint
Id=""{constraint.Id}""
PropDefId=""{constraint.PropertyDefinitionId}""
CatId=""{constraint.CategoryId}""
PropConstrTyp=""{constraint.Type}""
Val=""{constraint.Value}""/>";

    private static string CreateListValueBody(string value)
        => $@"<ListVal xsi:type=""xsd:string"">{value}</ListVal>";

    private static string CreateEntityClassContentSourcePropertyDefinitionBosy(EntityClassContentSourcePropertyDefinition definition)
        => $@"<EntClassCtntSrcPropDefs EntClassId=""{definition.EntityClass}"">
<CtntSrcPropDefArray>
{definition.ContentSourcePropertyDefinitions.Aggregate(new StringBuilder(),
    (builder, definition) => builder.Append(CreateContentSourcePropertyDefinition(definition)))}
</CtntSrcPropDefArray>
<MapTypArray>
{definition.MappingTypes.Aggregate(new StringBuilder(),
    (builder, type) => builder.Append($@"<MapTyp>{type}</MapTyp>"))}
</MapTypArray>
<PriorityArray>
{definition.Prioroties.Aggregate(new StringBuilder(),
    (builder, priority) => builder.Append($@"<Priority>{priority}</Priority>"))}
</PriorityArray>
<MapDirectionArray>
{definition.MappingDirections.Aggregate(new StringBuilder(),
    (builder, direction) => builder.Append($@"<MapDirection>{direction}</MapDirection>"))}
</MapDirectionArray>
<CanCreateNewArray>
{definition.CanCreateNew.Aggregate(new StringBuilder(),
    (builder, value) => builder.Append($@"<CreateNew>{value}</CreateNew>"))}
</CanCreateNewArray>
</EntClassCtntSrcPropDefs>";

    private static string CreateContentSourcePropertyDefinition(ContentSourcePropertyDefinition definition)
        => $@"<CtntSrcPropDef
CtntSrcId=""{definition.ContentSourceId}""
DispName=""{definition.DisplayName}""
Moniker=""{definition.Moniker}""
MapDirection=""{definition.MappingDirection}""
CanCreateNew=""{definition.CanCreateNew}""
Classification=""{definition.Classification}""
Typ=""{definition.DataType}""
CtntSrcDefTyp=""{definition.ContentSourceDefinitionType}""/>";
}
