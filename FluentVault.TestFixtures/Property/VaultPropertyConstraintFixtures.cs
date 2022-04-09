
using AutoFixture;

namespace FluentVault.TestFixtures;
public static partial class VaultResponseFixtures
{
    public static (string Body, VaultPropertyConstraint Constraint) GetVaultPropertyConstraintFixture()
    {
        Fixture fixture = new();
        fixture.Register(() => VaultPropertyConstraintType.MinimumLength);

        VaultPropertyConstraint constraint = fixture.Create<VaultPropertyConstraint>();
        string body = CreateVaultPropertyConstraintBody(constraint);

        return (body, constraint);
    }

    private static string CreateVaultPropertyConstraintBody(VaultPropertyConstraint constraint)
        => $@"<PropertyConstraint
Id=""{constraint.Id}""
PropDefId=""{constraint.PropertyDefinitionId}""
CatId=""{constraint.CategoryId}""
PropConstrTyp=""{constraint.Type}""
Val=""{constraint.Value}""/>";
}
