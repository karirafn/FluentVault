using FluentVault.RequestBuilders;

namespace FluentVault.Requests.Update;

internal class UpdateRequestBuilder : IRequestBuilder, IUpdateRequestBuilder, IUpdateFileRequestBuilder
{
    private readonly IUpdateFileLifecycleStateRequestBuilder _state;
    private readonly IUpdateFilePropertyDefinitionsRequestBuilder _property;

    public UpdateRequestBuilder(IUpdateFileLifecycleStateRequestBuilder state, IUpdateFilePropertyDefinitionsRequestBuilder property)
    {
        _state = state;
        _property = property;
    }

    public IUpdateFileRequestBuilder File => this;
    public IUpdateFileLifecycleStateRequestBuilder LifecycleState => _state;
    public IUpdateFilePropertyDefinitionsRequestBuilder PropertyDefinitions => _property;
}
