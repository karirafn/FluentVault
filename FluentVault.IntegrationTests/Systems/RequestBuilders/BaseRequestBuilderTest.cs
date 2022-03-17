
using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace FluentVault.IntegrationTests.Systems.RequestBuilders;
public abstract class BaseRequestBuilderTest : BaseSessionTest
{
    protected readonly IMediator _mediator;

    public BaseRequestBuilderTest() => 
        _mediator = new ServiceCollection()
        .AddFluentVault(options =>
        {
            options.Server = _options.Server;
            options.Database = _options.Database;
            options.Username = _options.Username;
            options.Password = _options.Password;
         })
        .BuildServiceProvider()
        .GetRequiredService<IMediator>();
}
