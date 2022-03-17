using System.Threading.Tasks;

using FluentVault.Features;

namespace FluentVault.IntegrationTests.Systems;
public abstract class BaseSessionTest : BaseIntegrationTest
{
    public override async Task InitializeAsync() => _session = await new SignInHandler(_service).Handle(new SignInCommand(_options), default);
}
