
using Ardalis.SmartEnum.AutoFixture;

using AutoFixture;

namespace FluentVault.TestFixtures;

public class SmartEnumFixture : Fixture
{
    public SmartEnumFixture()
    {
        Customize(new SmartEnumCustomization());
    }
}
