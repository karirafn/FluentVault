
using AutoFixture;

namespace FluentVault.UnitTests.Fixtures;
internal static partial class VaultResponseFixtures
{
    public static (string Body, VaultPropertyEntityClassContentSourcePropertyDefinition Definition) GetVaultPropertyEntityClassContentSourcePropertyDefinitionFixture()
    {
        Fixture fixture = new();
        fixture.Register(() => VaultEntityClass.File);
        fixture.Register(() => VaultPropertyAllowedMappingDirection.ReadAndWrite);
        fixture.Register(() => VaultPropertyClassification.Custom);
        fixture.Register(() => VaultDataType.DateTime);
        fixture.Register(() => VaultPropertyContentSourceDefinitionType.Component);
        fixture.Register(() => VaultPropertyMappingType.Constant);
        fixture.Register(() => VaultPropertyMappingDirection.Write);

        VaultPropertyEntityClassContentSourcePropertyDefinition definition = fixture.Create<VaultPropertyEntityClassContentSourcePropertyDefinition>();
        string body = CreateVaultPropertyEntityClassContentSourcePropertyDefinitionBody(definition);

        return (body, definition);
    }

    private static string CreateVaultPropertyEntityClassContentSourcePropertyDefinitionBody(VaultPropertyEntityClassContentSourcePropertyDefinition definition)
        => $@"<EntClassCtntSrcPropDefs EntClassId=""{definition.EntityClass}"">
<CtntSrcPropDefArray>
    {CreateEntityBody(definition.ContentSourcePropertyDefinitions, CreateVaultPropertyContentSourcePropertyDefinitionBody)}
</CtntSrcPropDefArray>
<MapTypArray>
    {CreateEntityBody(definition.MappingTypes, type => $@"<MapTyp>{type}</MapTyp>")}
</MapTypArray>
<PriorityArray>
    {CreateEntityBody(definition.Prioroties, priority => $@"<Priority>{priority}</Priority>")}
</PriorityArray>
<MapDirectionArray>
    {CreateEntityBody(definition.MappingDirections, direction => $@"<MapDirection>{direction}</MapDirection>")}
</MapDirectionArray>
<CanCreateNewArray>
    {CreateEntityBody(definition.CanCreateNew, value => $@"<CreateNew>{value}</CreateNew>")}
</CanCreateNewArray>
</EntClassCtntSrcPropDefs>";
}
