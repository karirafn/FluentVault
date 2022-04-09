
using AutoFixture;

namespace FluentVault.TestFixtures;
public static partial class VaultResponseFixtures
{
    public static (string Body, IEnumerable<VaultProperty> Files) GetVaultPropertyFixtures(int count)
    {
        Fixture fixture = new();
        fixture.Register(() => VaultPropertyAllowedMappingDirection.Read);
        fixture.Register(() => VaultPropertyClassification.Standard);
        fixture.Register(() => VaultPropertyContentSourceDefinitionType.File);
        fixture.Register(() => VaultDataType.String);
        fixture.Register(() => VaultEntityClass.File);
        fixture.Register(() => VaultPropertyMappingDirection.Write);
        fixture.Register(() => VaultPropertyMappingType.Constant);
        fixture.Register(() => VaultPropertyConstraintType.RequiresValue);

        return CreateBody<VaultProperty>(fixture, count, "GetPropertyDefinitionInfosByEntityClassId", "http://AutodeskDM/Services/Property/1/7/2020/", CreateVaultPropertyBody);
    }

    private static string CreateVaultPropertyBody(VaultProperty property)
        => $@"<PropDefInfo>
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
                        {CreateEntityBody(property.Definition.EntityClassAssociations, CreateVaultPropertyEntityClassAssociationBody)}
				    </EntClassAssocArray>
			    </PropDef>
                <ListValArray>
                    {CreateEntityBody(property.ListValues, value => $@"<ListVal xsi:type=""xsd:string"">{value}</ListVal>")}
                </ListValArray>
                <PropConstrArray>
                    {CreateEntityBody(property.Constraints, CreateVaultPropertyConstraintBody)}
                </PropConstrArray>
                <CtntSrcPropDefArray>
                    {CreateEntityBody(property.EntityClassContentSourcePropertyDefinitions, CreateVaultPropertyEntityClassContentSourcePropertyDefinitionBody)}
                </CtntSrcPropDefArray>
            </PropDefInfo>";
}
