
using AutoFixture;

namespace FluentVault.UnitTests.Fixtures;
internal static partial class VaultResponseFixtures
{
    public static (string Body, VaultProeprtyContentSourcePropertyDefinition Definition) GetVaultPropertyContentSourcePropertyDefinitionFixture()
    {
        Fixture fixture = new();
        fixture.Register(() => VaultPropertyAllowedMappingDirection.ReadAndWrite);
        fixture.Register(() => VaultPropertyClassification.Standard);
        fixture.Register(() => VaultDataType.Numeric);
        fixture.Register(() => VaultPropertyContentSourceDefinitionType.File);

        VaultProeprtyContentSourcePropertyDefinition definition = fixture.Create<VaultProeprtyContentSourcePropertyDefinition>();
        string body = CreateVaultPropertyContentSourcePropertyDefinitionBody(definition);

        return (body, definition);
    }

    private static string CreateVaultPropertyContentSourcePropertyDefinitionBody(VaultProeprtyContentSourcePropertyDefinition definition)
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
